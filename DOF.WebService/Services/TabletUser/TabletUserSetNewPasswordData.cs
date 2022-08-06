using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.TabletUser
{
    public class TabletUserSetNewPasswordData
    {        
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
