using DOF.WebService.Services.OilField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Models
{
    public class OilFieldModel: OilFieldCommonData
    {
        public int Id { get; set; }
        public new UserModel CreatedByModel { get; set; }

        public new UserModel DeletedByModel { get; set; }
    }
}
