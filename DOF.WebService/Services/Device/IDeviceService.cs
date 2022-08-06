using DOF.WebService.Common;
using DOF.WebService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device
{
    public interface IDeviceService
    {
        Task<int> Add(DeviceAddData deviceAddData);
        Task Update(DeviceUpdateData deviceUpdateData);
        Task Delete(int id, int deletedBy, DateTime deletedAt);
        Task Activate(Guid token, string clientAppVersion);
        Task<DeviceModel> GetById(int id);
        Task<DeviceModel> GetByToken(Guid token);
        Task<IEnumerable<DeviceModel>> Get(DeviceSearchData deviceSearchData, Pagination pagination, IEnumerable<Sort> sorts);
    }
}
