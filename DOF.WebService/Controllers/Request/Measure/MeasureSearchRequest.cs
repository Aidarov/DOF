
using DOF.WebService.Common;

namespace DOF.WebService.Controllers.Request.Measure
{
    public class MeasureSearchRequest : RequestWithPaginationAndSorting
    {
        public int? CreatedBy { get; set; }
        public Range CreatedAt { get; set; }
        public Range AcceptedAt { get; set; }
        public int? OilWellId { get; set; }
        public int? DeviceId { get; set; }
    }
}
