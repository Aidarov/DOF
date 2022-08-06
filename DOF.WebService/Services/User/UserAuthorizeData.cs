using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.User
{
    public class UserAuthorizeData
    {
        public string Login { get; set; }
        public string IpAddress { get; set; }
        public string Password { get; set; }
    }
}
