using System;

namespace DOF.WebService.Services.User
{
    public class UserAuthorizeResponse
    {
        public Guid Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
    }
}
