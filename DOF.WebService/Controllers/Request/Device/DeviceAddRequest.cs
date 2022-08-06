using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.Device
{
    public class DeviceAddRequest
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string PhotoPath { get; set; }
        public Guid Token { get; set; }
    }
}
