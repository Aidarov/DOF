using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    public class MainController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IDeviceService _deviceService;
        public MainController(IUserService userService, IUserSessionService userSessionService, IDeviceService deviceService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
            _deviceService = deviceService;
        }

        protected async Task<int> GetUserId()
        {
            return 0;
            string userToken = HttpContext.Request.Headers["user-token"].ToString();
            Guid guidUserToken = Guid.Parse(userToken);

            var userSession = await _userSessionService.GetByToken(guidUserToken);
            return userSession.UserId;
        }

        protected async Task<int> GetDeviceId()
        {
            return 1;
            string deviceToken = HttpContext.Request.Headers["device-token"].ToString();
            Guid guidDeviceToken = Guid.Parse(deviceToken);

            var device = await _deviceService.GetByToken(guidDeviceToken);

            return device.Id;
        }

        protected string IpAddress { get { return HttpContext.Connection.RemoteIpAddress.ToString(); } }
    }
}