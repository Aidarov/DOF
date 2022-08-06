using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.User
{
    public class UserPasswordUpdateData : UserPasswordSetData
    {
        public string CurrentPassword { get; set; }
    }
}
