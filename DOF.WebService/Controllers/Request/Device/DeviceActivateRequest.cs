using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.Device
{
    public class DeviceActivateRequest
    {
        public Guid Token { get; set; }
        public string ClientAppVersion { get; set; }
    }
}
