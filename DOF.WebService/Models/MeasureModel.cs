using DOF.WebService.Services.Measure;

namespace DOF.WebService.Models
{
    public class MeasureModel : MeasureCommonData
    {
        public int Id { get; set; }
        public new TabletUserModel CreatedBy { get; set; }
        private new int DeviceId { get; set; }
        public DeviceModel Device { get; set; }
        private new int OilWellId { get; set; }
        public OilWellModel OilWell { get; set; }
    }
}
