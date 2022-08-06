using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.TabletUser
{
    public class TabletUserResetPassword : TabletUserSetNewPasswordData
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
    }
}
