using DOF.WebService.Common;

namespace DOF.WebService.Services.UserSession
{
    public class UserSessionSearchData
    {
        public int? UserId { get; set; }
        public bool? IsBlocked { get; set; }
        public Range CreatedAt { get; set; }
        public string IpAddress { get; set; }
    }
}