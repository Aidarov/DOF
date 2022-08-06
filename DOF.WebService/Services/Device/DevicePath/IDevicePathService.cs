using DOF.WebService.Common;
using DOF.WebService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device.DevicePath
{
    public interface IDevicePathService
    {
        Task<int> Add(DevicePathAddData devicePathAddData);
        Task AddRange(IEnumerable<DevicePathAddData> devicePathAddData);
        Task<IEnumerable< DevicePathModel>> Get(DevicePathSearchData filter, Pagination pagination, IEnumerable<Sort> sorts);
    }
}
