using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilWell
{
    public class OilWellSearchData
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public int? OilFieldId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
