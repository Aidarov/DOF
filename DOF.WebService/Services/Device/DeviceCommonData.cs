using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Device
{
    public class DeviceCommonData
    {
        public string Name { get; set; }
        public string Model { get; set; }        
        public string SerialNumber { get; set; }
        public string PhotoPath { get; set; }
        public Guid Token { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
