using DOF.WebService.Services.UserSession;
using System;

namespace DOF.WebService.Services.Mappings.UserSession
{
    public static class UserSessionMappings
    {
        public static Database.Models.UserSession MapToDbModel(this UserSessionAddData userSessionAddData)
        {
            return new Database.Models.UserSession()
            {
                CreatedAt = userSessionAddData.CreatedAt,
                ExpiresAt = userSessionAddData.ExpiresAt,
                IpAddress = userSessionAddData.IpAddress,
                IsBlocked = userSessionAddData.IsBlocked,
                Token = userSessionAddData.Token,
                UserId = userSessionAddData.UserId
            };
        }
    }
}
