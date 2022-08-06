using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.OilWell
{
    public class OilWellAddRequest
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public int OilField { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
