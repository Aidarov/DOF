using DOF.WebService.Services.Device;

namespace DOF.WebService.Models
{
    public class DeviceModel : DeviceCommonData
    {
        public int Id { get; set; }
        public new UserModel CreatedByModel { get; set; }
        public new UserModel DeletedByModel { get; set; }
    }
}
