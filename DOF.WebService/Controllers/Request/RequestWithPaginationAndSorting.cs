using DOF.WebService.Common;
using System.Collections.Generic;

namespace DOF.WebService.Controllers.Request
{
    public class RequestWithPaginationAndSorting
    {
        public Pagination Pagination { get; set; }
        public IEnumerable<Sort> Sorts { get; set; }
    }
}
