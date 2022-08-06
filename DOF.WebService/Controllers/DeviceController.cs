using System;
using System.Threading.Tasks;
using DOF.WebService.Attributes;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.Device;
using DOF.WebService.Database.Models;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : MainController
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService, IUserService userService, IUserSessionService userSessionService) : base(userService, userSessionService, deviceService)
        {
            _deviceService = deviceService;
        }


        [HttpPost("Add")]
        public async Task<ActionResult> Add(DeviceAddRequest deviceAddRequest)
        {
            if (!ModelState.IsValid)    return BadRequest();

            var deviceAddData = deviceAddRequest.MapToDeviceAddData(createdBy: await GetUserId(), createdAt: DateTime.Now, isDeleted: true);

            var result = await _deviceService.Add(deviceAddData);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(DeviceUpdateRequest deviceUpdateRequest)
        {
            if (!ModelState.IsValid)  return BadRequest();

            var deviceUpdateData = deviceUpdateRequest.MapToDeviceUpdateData();

            await _deviceService.Update(deviceUpdateData);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _deviceService.Delete(id: id, deletedBy: await GetUserId(), deletedAt: DateTime.Now);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _deviceService.GetById(id);

            return Ok(result);
        }

        [HttpPost("Get")]
        public async Task<IActionResult> Get(DeviceSearchRequest deviceSearchRequest)
        {
            var filter = deviceSearchRequest.MapToDeviceSearchData();

            var result = await _deviceService.Get(deviceSearchData: filter, pagination: deviceSearchRequest.Pagination, sorts: deviceSearchRequest.Sorts);

            return Ok(result);
        }

        [HttpPost("Activate")]
        public async Task<ActionResult> Activate(DeviceActivateRequest deviceActivateRequest)
        {
            await _deviceService.Activate(token: deviceActivateRequest.Token, clientAppVersion: deviceActivateRequest.ClientAppVersion);

            return Ok();
        }
    }
}