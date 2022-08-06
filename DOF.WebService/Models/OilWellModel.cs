using DOF.WebService.Services.OilWell;

namespace DOF.WebService.Models
{
    public class OilWellModel : OilWellCommonData
    {
        public int Id { get; set; }

        public new UserModel CreatedByModel { get; set; }

        public new UserModel DeletedByModel { get; set; }

        public new OilFieldModel OilField { get; set; } 
    }
}
