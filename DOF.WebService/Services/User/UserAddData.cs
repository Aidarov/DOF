using DOF.WebService.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.User
{
    public class UserAddData : UserCommonData
    {
        public int CreatedBy { get; set; }
    }
}
