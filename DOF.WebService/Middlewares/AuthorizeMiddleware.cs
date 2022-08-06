using DOF.WebService.Attributes;
using DOF.WebService.Database;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DOF.WebService.Middlewares
{
    public class AuthorizeMiddleware
    {
        private RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task Invoke(HttpContext context, IUserSessionService userSessionService, IUserService userService)
        {
            //var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();

            if (attribute != null)
            {
                var userTokenInString = context.Request.Headers["user-token"].ToString();

                //if user token is null
                if (String.IsNullOrEmpty(userTokenInString))
                {
                    await ReturnUnauthorizedResponse(context);

                    return;
                }

                var userTokenInGuid = Guid.Parse(userTokenInString);

                var userSession = await userSessionService.GetByToken(userTokenInGuid);
                //if token not found in database
                if(userSession == null)
                {
                    await ReturnUnauthorizedResponse(context);

                    return;
                }

                var user = await userService.GetById(userSession.UserId);

                var allowedRoles = attribute.Roles;
                //if current users role not allowed
                if (!allowedRoles.Any(c => c == user.Role))
                {
                    await ReturnForbiddenResponse(context);

                    return;
                }
            }

            await _next(context);
        }

        public async Task ReturnUnauthorizedResponse(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }

        public async Task ReturnForbiddenResponse(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            await context.Response.WriteAsync("Forbidden");
        }


    }
}
