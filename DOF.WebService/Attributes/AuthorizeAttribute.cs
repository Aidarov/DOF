using DOF.WebService.Database.Models;
using System;

namespace DOF.WebService.Attributes
{
    public class AuthorizeAttribute : Attribute
    {
        public Role[] Roles { get; }
        public AuthorizeAttribute(Role[] roles)
        {
            Roles = roles;
        }
    }
}
