using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.User
{
    public class UserUpdateRequest : UserAddRequest
    {
        public int Id { get; set; }
    }
}
