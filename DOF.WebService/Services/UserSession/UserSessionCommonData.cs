using System;

namespace DOF.WebService.Services.UserSession
{
    public class UserSessionCommonData
    {
        public int UserId { get; set; }
        public Guid Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string IpAddress { get; set; }
        public bool IsBlocked { get; set; }
    }
}
