using DOF.WebService.Common;
using DOF.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.TabletUser
{
    public interface ITabletUserService
    {
        Task<int> Add(TabletUserAddData tabletUserAddData);
        Task Update(TabletUserUpdateData tabletUserUpdateData);
        Task Delete(int id, int deletedBy, DateTime deletedAt);
        Task<TabletUserModel> GetById(int id);
        Task<IEnumerable<TabletUserModel>> Get(TabletUserSearchData filter, Pagination pagination, IEnumerable<Sort> sorts);
    }
}
