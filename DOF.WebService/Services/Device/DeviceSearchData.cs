using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device
{
    public class DeviceSearchData
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Token { get; set; }
        public int? CreatedBy { get; set; }
    }
}
