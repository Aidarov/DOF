using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.OilWell
{
    public class OilWellSearchRequest : RequestWithPaginationAndSorting
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public int? OilFieldId { get; set; }
    }
}
