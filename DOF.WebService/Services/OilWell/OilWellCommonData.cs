using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilWell
{
    public class OilWellCommonData
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public int OilField { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
