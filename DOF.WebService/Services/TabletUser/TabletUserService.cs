
using DOF.WebService.Common;
using DOF.WebService.CryptoServices;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.TabletUser;
using DOF.WebService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.TabletUser
{
    public class TabletUserService : ITabletUserService
    {
        private MainDbContext _context { get; set; }
        public TabletUserService(MainDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(TabletUserAddData tabletUserAddData)
        {
            if (tabletUserAddData.PasswordData.NewPassword != tabletUserAddData.PasswordData.ConfirmPassword)
                throw new TabletUserConfirmationFailedException();

            var userExist = await _context.TabletUsers.AnyAsync(c => c.Login.StartsWith(tabletUserAddData.Login, StringComparison.OrdinalIgnoreCase));

            if (userExist)
                throw new TabletUserAllreadyExistException();

            var salt = PasswordService.GenerateSalt();

            var newTabletUser = new Database.Models.TabletUser
            {
                FirstName = tabletUserAddData.FirstName,
                LastName = tabletUserAddData.LastName,
                MiddleName = tabletUserAddData.MiddleName,
                Login = tabletUserAddData.Login,
                Salt = PasswordService.GetSaltInBase64(salt),
                Password = PasswordService.GeneratePassword(tabletUserAddData.PasswordData.NewPassword, salt)
            };

            await _context.TabletUsers.AddAsync(newTabletUser);

            await _context.SaveChangesAsync();

            return newTabletUser.Id;
        }

        public async Task Delete(int id, int deletedBy, DateTime deletedAt)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TabletUserModel>> Get(TabletUserSearchData filter, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<TabletUserModel> result = null;

            var set = _context.TabletUsers.AsQueryable();

            if (!String.IsNullOrEmpty(filter.Login))
                set = set.Where(c => c.Login.StartsWith(filter.Login, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(filter.FirstName))
                set = set.Where(c => c.FirstName.StartsWith(filter.FirstName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(filter.LastName))
                set = set.Where(c => c.LastName.StartsWith(filter.LastName, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(filter.MiddleName))
                set = set.Where(c => c.MiddleName.StartsWith(filter.MiddleName, StringComparison.OrdinalIgnoreCase));

            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(TabletUserModel.Login)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.Login) : set.OrderByDescending(c => c.Login);
                else if (sort.FieldName.Equals(nameof(TabletUserModel.FirstName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.FirstName) : set.OrderByDescending(c => c.FirstName);
                else if (sort.FieldName.Equals(nameof(TabletUserModel.LastName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.LastName) : set.OrderByDescending(c => c.LastName);
                else if (sort.FieldName.Equals(nameof(TabletUserModel.MiddleName)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.MiddleName) : set.OrderByDescending(c => c.MiddleName);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            foreach (var rawTabUser in resultSet)
                result.Add(await Convert(rawTabUser));

            return result;            
        }

        public async Task<TabletUserModel> GetById(int id)
        {
            var userRaw =  await _context.TabletUsers.SingleAsync(c => c.Id == id);
            return await Convert(userRaw);

        }

        private async Task<TabletUserModel> Convert(Database.Models.TabletUser userRaw)
        {
            return new TabletUserModel()
            {
                Id = userRaw.Id,
                FirstName = userRaw.FirstName,
                LastName = userRaw.LastName,
                Login = userRaw.Login,
                MiddleName = userRaw.MiddleName
            };
        }

        public async Task Update(TabletUserUpdateData tabletUserUpdateData)
        {
            throw new NotImplementedException();
        }
    }
}
