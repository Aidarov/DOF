using DOF.WebService.Database;
using DOF.WebService.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Models
{
    public class UserModel : UserCommonData
    {
        public int Id { get; set; }
        public new CreatedByModel CreatedBy { get; set; }
    }

    public class CreatedByModel : UserCommonData
    {

    }
}
