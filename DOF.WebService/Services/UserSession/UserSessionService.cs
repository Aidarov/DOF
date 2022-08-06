using DOF.WebService.Common;
using DOF.WebService.Database;
using DOF.WebService.Exceptions.UserSession;
using DOF.WebService.Models;
using DOF.WebService.Services.Mappings.UserSession;
using DOF.WebService.Services.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.UserSession
{
    public class UserSessionService : IUserSessionService
    {
        private readonly MainDbContext _context;
        //private readonly IUserService _userService;

        public UserSessionService(MainDbContext context/*, IUserService userService*/)
        {
            _context = context;
            //_userService = userService;
        }
        public async Task<int> Add(UserSessionAddData userSessionAddData)
        {
            if (_context.UserSessions.Any(c => c.Token == userSessionAddData.Token))
                throw new UserSessionAllreadyExistException($"User session with token = {userSessionAddData.Token} allready exist.");

            var model = userSessionAddData.MapToDbModel();

            await _context.UserSessions.AddAsync(model);

            await _context.SaveChangesAsync();

            return model.Id;
        }

        public async Task Block(Guid token)
        {
            if (_context.UserSessions.Any(c => c.Token == token))
                throw new UserSessionNotFoundException($"User session not found with token = {token}.");

            var userSession = _context.UserSessions.Single(c => c.Token == token);

            userSession.IsBlocked = true;

            await _context.SaveChangesAsync();

        }

        public async Task<UserSessionModel> GetByToken(Guid token)
        {
            var userSession = await _context.UserSessions.SingleOrDefaultAsync(c => c.Token == token && c.IsBlocked == false && c.ExpiresAt > DateTime.Now);

            if (userSession == null)
                return null;
            else
                return Convert(userSession);
        }

        public async Task<bool> IsTokenActive(Guid token)
        {
            var userSessionModel = await GetByToken(token);

            return userSessionModel.IsBlocked;
        }

        public async Task<IEnumerable<UserSessionModel>> Get(UserSessionSearchData userSessionSearchData, Pagination pagination, IEnumerable<Sort> sorts)
        {
            List<UserSessionModel> result = null;

            var set = _context.UserSessions.AsQueryable();

            if (userSessionSearchData.UserId != null)
                set = set.Where(c => c.UserId == userSessionSearchData.UserId);

            if (userSessionSearchData.IsBlocked != null)
                set = set.Where(c => c.IsBlocked == userSessionSearchData.IsBlocked);

            if (userSessionSearchData.CreatedAt != null && userSessionSearchData.CreatedAt.From != null)
                set = set.Where(c => c.CreatedAt >= userSessionSearchData.CreatedAt.From.Value);

            if (userSessionSearchData.CreatedAt != null && userSessionSearchData.CreatedAt.To != null)
                set = set.Where(c => c.CreatedAt <= userSessionSearchData.CreatedAt.To.Value);

            if (userSessionSearchData.IpAddress != null)
                set = set.Where(c => c.IpAddress.Equals(userSessionSearchData.IpAddress));


            foreach (var sort in sorts?.ToList())
            {
                if (sort.FieldName.Equals(nameof(Database.Models.UserSession.IpAddress)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.IpAddress) : set.OrderByDescending(c => c.IpAddress);

                if (sort.FieldName.Equals(nameof(Database.Models.UserSession.IsBlocked)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.IsBlocked) : set.OrderByDescending(c => c.IsBlocked);

                if (sort.FieldName.Equals(nameof(Database.Models.UserSession.UserId)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.UserId) : set.OrderByDescending(c => c.UserId);

                if (sort.FieldName.Equals(nameof(Database.Models.UserSession.CreatedAt)))
                    set = sort.Direction == SortDirection.ASC ? set.OrderBy(c => c.CreatedAt) : set.OrderByDescending(c => c.CreatedAt);
            }

            var resultSet = await set.Skip(pagination.PageNumber * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            if (resultSet.Any())
                result = new List<UserSessionModel>();

            foreach (var rawDevice in resultSet)
                result.Add(Convert(rawDevice));

            return result;
        }

        private  UserSessionModel Convert(Database.Models.UserSession userSession)
        {
            return new UserSessionModel()
            {
                Id = userSession.Id,
                CreatedAt = userSession.CreatedAt,
                ExpiresAt = userSession.ExpiresAt,
                IpAddress = userSession.IpAddress,
                Token = userSession.Token,
                IsBlocked = userSession.IsBlocked,
                UserId = userSession.UserId                
            };
        }
    }
}
