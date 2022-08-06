using System;

namespace DOF.WebService.Controllers.Request.DevicePath
{
    public class DevicePathSearchRequest : RequestWithPaginationAndSorting
    {
        public int? DeviceId { get; set; }
        public Common.Range AcceptedAt { get; set; }
    }
}
