using DOF.WebService.Common;
using DOF.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilWell
{
    public interface IOilWellService
    {
        Task<int> Add(OilWellAddData oilWellAddData);
        Task Update(OilWellUpdateData oilWellUpdate);
        Task Delete(int id, int deletedBy, DateTime deletedAt);
        Task<OilWellModel> GetById(int id);
        Task<IEnumerable<OilWellModel>> Get(OilWellSearchData oilWellSearchData, Pagination pagination, IEnumerable<Sort> sorts);
    }
}
