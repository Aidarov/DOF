using DOF.WebService.Controllers.Request.Device;
using DOF.WebService.Services.Device;
using System;

namespace DOF.WebService.Controllers.Mappings
{
    public static class DeviceRequestMappings
    {
        public static DeviceAddData MapToDeviceAddData(this DeviceAddRequest request, int createdBy, DateTime createdAt, bool isDeleted)
        {
            return new DeviceAddData()
            {
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                IsDeleted = isDeleted,
                Model = request.Model,
                Name = request.Name,
                PhotoPath = request.PhotoPath,
                SerialNumber = request.SerialNumber,
                Token = request.Token
            };
        }

        public static DeviceUpdateData MapToDeviceUpdateData(this DeviceUpdateRequest request)
        {
            return new DeviceUpdateData()
            {
                Id = request.Id,
                Model = request.Model,
                Name = request.Name,
                PhotoPath = request.PhotoPath,
                SerialNumber = request.SerialNumber,
                Token = request.Token                
            };
        }

        public static DeviceSearchData MapToDeviceSearchData(this DeviceSearchRequest request)
        {
            return new DeviceSearchData()
            {
                Name = request.Name,
                Model = request.Model,
                Token = request.Token,
                SerialNumber = request.SerialNumber,
                CreatedBy = request.CreatedBy
            };
        }
    }
}
