using DOF.WebService.Common;
using DOF.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilField
{
    public interface IOilFieldService
    {
        Task<OilFieldModel> GetById(int id);
        Task<IEnumerable<OilFieldModel>> Get(OilFieldSearchData oilFieldSearchData, Pagination pagination, IEnumerable<Sort> sorts);
        Task<int> Add(OilFieldAddData oilFieldAddData);
        Task Update(OilFieldUpdateData oilFieldUpdateData);
        Task Delete(int id, int deletedBy, DateTime deletedAt);
    }
}
