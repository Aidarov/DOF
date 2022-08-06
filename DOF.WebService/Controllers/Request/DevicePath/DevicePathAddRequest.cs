using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.DevicePath
{
    public class DevicePathAddRequest
    {
        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime AcceptedAt { get; set; }

        public string Uid { get; set; }
    }
}
