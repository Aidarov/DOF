using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.OilField
{
    public class OilFieldSearchData
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
