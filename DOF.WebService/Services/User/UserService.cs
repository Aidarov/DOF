using DOF.WebService.Common;
using DOF.WebService.CryptoServices;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.User;
using DOF.WebService.Models;
using DOF.WebService.Services.UserSession;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DOF.WebService.Services.User
{
    public class UserService : IUserService
    {
        private readonly MainDbContext _context;
        private readonly IUserSessionService _userSessionService;
        public UserService(MainDbContext mainDbContext, IUserSessionService userSessionService)
        {
            _context = mainDbContext;
            _userSessionService = userSessionService;
        }
        public async Task<int> Add(UserAddData userAddData)
        {
            var userExist = await _context.Users.AsNoTracking().AnyAsync(c => c.Login.Contains(userAddData.Login) || c.Email.Contains(userAddData.Email));

            if (!userExist)
            {
                var newUser = new Database.Models.User()
                {
                    Login = userAddData.Login,
                    Email = userAddData.Email,
                    FirstName = userAddData.FirstName,
                    LastName = userAddData.LastName,
                    MiddleName = userAddData.MiddleName,
                    IsBlocked = userAddData.IsBlocked,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userAddData.CreatedBy
                };

                await _context.Users.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return newUser.Id;
            }

            throw new UserAllReadyExist();
        }

        public async Task Block(long userId)
        {
            var user = await _context.Users.SingleAsync(c => c.Id ==  userId && c.IsBlocked == false);
            user.IsBlocked = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> Get(UserSearchData userSearchData, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<UserModel> result = null;

            var set = _context.Users.AsQueryable();

            if (!String.IsNullOrEmpty(userSearchData.Email))
                set = set.Where(c => c.Email.StartsWith(userSearchData.Email, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(userSearchData.Login))
                set = set.Where(c => c.Login.StartsWith(userSearchData.Login, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(userSearchData.FirstName))
                set = set.Where(c => c.FirstName.StartsWith(userSearchData.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(userSearchData.LastName))
                set = set.Where(c => c.LastName.StartsWith(userSearchData.LastName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(userSearchData.MiddleName))
                set = set.Where(c => c.MiddleName.StartsWith(userSearchData.MiddleName, StringComparison.OrdinalIgnoreCase));

            if (userSearchData.IsBlocked != null)
                set = set.Where(c => c.IsBlocked == userSearchData.IsBlocked);

            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(UserModel.Email)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Email) : set.OrderByDescending(c => c.Email);
                else if (sort.FieldName.Equals(nameof(UserModel.Login)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Login) : set.OrderByDescending(c => c.Login);
                else if (sort.FieldName.Equals(nameof(UserModel.FirstName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.FirstName) : set.OrderByDescending(c => c.FirstName);
                else if (sort.FieldName.Equals(nameof(UserModel.LastName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.LastName) : set.OrderByDescending(c => c.LastName);
                else if (sort.FieldName.Equals(nameof(UserModel.MiddleName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.MiddleName) : set.OrderByDescending(c => c.MiddleName);
                else if (sort.FieldName.Equals(nameof(UserModel.Role)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Role) : set.OrderByDescending(c => c.Role);
                else
                    throw new Exception("Error occured");
            }
            


            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            /*var set = _mainDbContext.Users.Where(c => 
               (!String.IsNullOrEmpty(userSearchData.Email) ? c.Email.StartsWith(userSearchData.Email, StringComparison.OrdinalIgnoreCase) : true) &&
               (!String.IsNullOrEmpty(userSearchData.FirstName) ? c.Email.StartsWith(userSearchData.FirstName, StringComparison.OrdinalIgnoreCase) : true) &&
               (!String.IsNullOrEmpty(userSearchData.LastName) ? c.Email.StartsWith(userSearchData.LastName, StringComparison.OrdinalIgnoreCase) : true) &&
               (!String.IsNullOrEmpty(userSearchData.MiddleName) ? c.Email.StartsWith(userSearchData.MiddleName, StringComparison.OrdinalIgnoreCase) : true) &&
               (userSearchData.IsBlocked != null ? userSearchData.IsBlocked == true : true) &&);*/

            return result;
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.Id == id && c.IsBlocked == false);

            if (user != null)
            {
                return await ConvertUserToUserModel(user);
            }

            return null;
        }

        

        public async Task Update(UserUpdateData userUpdateData)
        {
            var loginIsBusy = await _context.Users.AnyAsync(c => c.Login.Contains(userUpdateData.Login, StringComparison.OrdinalIgnoreCase) && c.Id != userUpdateData.Id);

            if (loginIsBusy)
                throw new UserLoginIsBusyException();

            var emailIsBusy = await _context.Users.AnyAsync(c => c.Email.Contains(userUpdateData.Email, StringComparison.OrdinalIgnoreCase) && c.Id != userUpdateData.Id);

            if (emailIsBusy)
                throw new UserEmailIsBusyException();

            var currentUser = await _context.Users.SingleAsync(c => c.Id == userUpdateData.Id);

            currentUser.Login = userUpdateData.Login;
            currentUser.Email = userUpdateData.Email;
            currentUser.FirstName = userUpdateData.FirstName;
            currentUser.LastName = userUpdateData.LastName;
            currentUser.MiddleName = userUpdateData.MiddleName;
            currentUser.Role = userUpdateData.Role;
            currentUser.IsBlocked = userUpdateData.IsBlocked;

            await _context.SaveChangesAsync();
        }

        public async Task SetNewPassword(UserPasswordSetData userPwdSetData)
        {
            if (userPwdSetData.NewPassword != userPwdSetData.ConfirmPassword)
            {
                throw new PasswordConfirmationFailed();
            }

            var salt = PasswordService.GenerateSalt();

            var user = await _context.Users.SingleAsync(c => c.Id == userPwdSetData.UserId);

            user.Salt = PasswordService.GetSaltInBase64(salt);
            user.Password = PasswordService.GeneratePassword(userPwdSetData.NewPassword, salt);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassword(UserPasswordUpdateData userPwdUpdData)
        {
            if (userPwdUpdData.NewPassword != userPwdUpdData.ConfirmPassword)
            {
                throw new PasswordConfirmationFailed();
            }

            var user = await _context.Users.SingleAsync(c => c.Id == userPwdUpdData.UserId);

            var saltInBytes = Convert.FromBase64String(user.Salt);

            var currentPassword = PasswordService.GeneratePassword(userPwdUpdData.CurrentPassword, saltInBytes);

            if (user.Password != currentPassword)
            {
                throw new IncorrectCurrentPassword();
            }

            await SetNewPassword(new UserPasswordSetData()
            {
                UserId = userPwdUpdData.UserId,
                NewPassword = userPwdUpdData.NewPassword,
                ConfirmPassword = userPwdUpdData.ConfirmPassword
            });            
        }

        private async Task<UserModel> ConvertUserToUserModel(Database.Models.User user)
        {
            UserModel createdBy = null;

            if (user.CreatedBy != null)
                createdBy = await GetById(user.CreatedBy ?? 0);

            return new UserModel()
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                CreatedBy = (user.CreatedBy != null) ? new CreatedByModel()
                {
                    CreatedAt = createdBy.CreatedAt,
                    CreatedBy = null,
                    Email = createdBy.Email,
                    Login = createdBy.Login,
                    FirstName = createdBy.FirstName,
                    LastName = createdBy.LastName,
                    MiddleName = createdBy.MiddleName,
                    IsBlocked = createdBy.IsBlocked,
                    Role = createdBy.Role
                } : null,
                Email = user.Email,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                IsBlocked = user.IsBlocked,
                Role = user.Role
            };
        }

        public async Task<UserAuthorizeResponse> Authorize(UserAuthorizeData userAuthorizeData)
        {
            var user = await _context.Users.SingleAsync(c => c.Login == userAuthorizeData.Login);

            var saltInBytes = Convert.FromBase64String(user.Salt);

            var currentPassword = PasswordService.GeneratePassword(userAuthorizeData.Password, saltInBytes);

            if (user.Password != currentPassword)
            {
                throw new IncorrectCurrentPassword();
            }

            var session = new UserSessionAddData()
            {
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(15),
                IpAddress = userAuthorizeData.IpAddress,
                IsBlocked = false,
                Token = Guid.NewGuid(),
                UserId = user.Id
            };

            var result = await _userSessionService.Add(session);

            await _context.SaveChangesAsync();

            return new UserAuthorizeResponse()
            {
                Token = session.Token,
                ExpiresAt = session.ExpiresAt,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Login = user.Login
            };
        }
    }
}
