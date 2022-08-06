using DOF.WebService.Common;
using DOF.WebService.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.User
{
    public class UserSearchData
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool? IsBlocked { get; set; }
        public Role? Role { get; set; }
    }
}
