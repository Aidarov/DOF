using DOF.WebService.Common;
using DOF.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.UserSession
{
    public interface IUserSessionService
    {
        Task<int> Add(UserSessionAddData userSessionAddData);
        Task<UserSessionModel> GetByToken(Guid token);
        Task<bool> IsTokenActive(Guid token);
        Task Block(Guid token);
        Task<IEnumerable<UserSessionModel>> Get(UserSessionSearchData userSessionSearchData, Pagination pagination, IEnumerable<Sort> sorts);
    }
}
