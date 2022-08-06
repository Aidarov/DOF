using DOF.WebService.Services.Device.DevicePath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Models
{
    public class DevicePathModel : DevicePathCommonData
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
    }
}
