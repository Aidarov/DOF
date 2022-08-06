using System;

namespace DOF.WebService.Exceptions.UserSession
{
    public class UserSessionNotFoundException : Exception
    {
        public UserSessionNotFoundException(string message) : base(message)
        {

        }
    }
}
