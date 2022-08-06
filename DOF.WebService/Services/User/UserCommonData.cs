using DOF.WebService.Database.Models;
using System;

namespace DOF.WebService.Services.User
{
    public class UserCommonData
    {
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public bool IsBlocked { get; set; }
        public Role Role { get; set; }
    }
}
