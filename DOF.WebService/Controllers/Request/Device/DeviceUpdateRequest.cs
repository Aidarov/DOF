using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.Device
{
    public class DeviceUpdateRequest : DeviceAddRequest
    {
        public int Id { get; set; }
    }
}
