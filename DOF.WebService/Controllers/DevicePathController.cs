using System.Collections.Generic;
using System.Threading.Tasks;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.DevicePath;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.Device.DevicePath;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicePathController : MainController
    {
        private readonly IDevicePathService _devicePathService;
        public DevicePathController(IDevicePathService devicePathService, 
            IUserService userService, 
            IUserSessionService userSessionService,
            IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _devicePathService = devicePathService;
        }

        [HttpPost("AddRange")]
        public async Task<ActionResult> AddRange(IEnumerable<DevicePathAddRequest> devicePathAddRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var devicePathList = new List<DevicePathAddData>();

            foreach (var data in devicePathAddRequest)
                devicePathList.Add(data.MapToDevicePathAddData(deviceId: await GetDeviceId()));

            await _devicePathService.AddRange(devicePathList);

            return Ok();
        }

        [HttpGet("Get")]
        public async Task<ActionResult> Get(DevicePathSearchRequest devicePathSearchRequest)
        {
            var result = await _devicePathService.Get(filter: new DevicePathSearchData()
            {
                AcceptedAt = devicePathSearchRequest.AcceptedAt,
                DeviceId = devicePathSearchRequest.DeviceId,
                
            }, pagination: devicePathSearchRequest.Pagination, sorts: devicePathSearchRequest.Sorts);

            return Ok(result);
        }
    }
}