using DOF.WebService.Services.Device.DevicePath;

namespace DOF.WebService.Services.Mappings.DevicePath
{
    public static class DevicePathMappings
    {
        public static Database.Models.DevicePath MapToDbModel(this DevicePathAddData devicePathAddData)
        {
            return new Database.Models.DevicePath()
            {
                AcceptedAt = devicePathAddData.AcceptedAt,
                DeviceId = devicePathAddData.DeviceId,
                Latitude = devicePathAddData.Latitude,
                Longitude = devicePathAddData.Longitude,
                TimeStamp = devicePathAddData.TimeStamp,
                Uid = devicePathAddData.Uid
            }; 
        }

    }
}
