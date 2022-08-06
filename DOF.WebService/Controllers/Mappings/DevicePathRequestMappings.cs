using DOF.WebService.Controllers.Request.DevicePath;
using DOF.WebService.Services.Device.DevicePath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Mappings
{
    public static class DevicePathRequestMappings
    {
        public static DevicePathAddData MapToDevicePathAddData(this DevicePathAddRequest devicePathAddRequest, int deviceId)
        {
            return new DevicePathAddData()
            {
                AcceptedAt = devicePathAddRequest.AcceptedAt,
                DeviceId = deviceId,
                Latitude = devicePathAddRequest.Latitude,
                Longitude = devicePathAddRequest.Longitude,
                TimeStamp = devicePathAddRequest.TimeStamp,
                Uid = devicePathAddRequest.Uid
            };
        }
    }
}
