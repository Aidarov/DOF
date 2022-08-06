using DOF.WebService.Common;
using DOF.WebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Measure
{
    public interface IMeasureService
    {
        Task<IEnumerable<MeasureModel>> Get(MeasureSearchData filter, Pagination pagination, IEnumerable<Sort> sorts);
        Task AddRange(IEnumerable<MeasureAddData> measureAddCollections);
    }
}
