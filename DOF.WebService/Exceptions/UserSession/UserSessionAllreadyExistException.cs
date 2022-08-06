using System;

namespace DOF.WebService.Exceptions.UserSession
{
    public class UserSessionAllreadyExistException : Exception
    {
        public UserSessionAllreadyExistException(string message) : base(message)
        {

        }
    }
}
