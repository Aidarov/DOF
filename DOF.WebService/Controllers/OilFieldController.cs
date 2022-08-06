using System;
using System.Threading.Tasks;
using DOF.WebService.Attributes;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.OilField;
using DOF.WebService.Database.Models;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.OilField;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilFieldController : MainController
    {
        private readonly IOilFieldService _oilFieldService;
        public OilFieldController(IOilFieldService oilFieldService, IUserService userService,
            IUserSessionService userSessionService,
            IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _oilFieldService = oilFieldService;
        }

        [HttpPost("Add")]
        //[Authorize(new Role[] { Role.Admin })]
        public async Task<ActionResult> Add(OilFieldAddRequest oilFieldAddRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentUserId = await GetUserId();

            var oilFieldAddData = oilFieldAddRequest.MapToOilFieldAddData(createdBy: currentUserId, createdAt: DateTime.Now, isDeleted: false);

            var result = await _oilFieldService.Add(oilFieldAddData);

            return Ok();
        }

        [HttpPut("Add")]
        public async Task<ActionResult> Update(OilFieldUpdateRequest oilFieldUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var oilFieldUpdateData = oilFieldUpdateRequest.MapToOilFieldUpdateData();

            await _oilFieldService.Update(oilFieldUpdateData);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var currentUserId = await GetUserId();

            await _oilFieldService.Delete(id: id, deletedBy: currentUserId, deletedAt: DateTime.Now);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _oilFieldService.GetById(id);

            return Ok(result);
        }

        [HttpPost("Get")]
        public async Task<ActionResult> Get(OilFieldSearchRequest oilFieldSearchRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var oilFieldSearchData = oilFieldSearchRequest.MapToOilFieldSearchData();

            var result = await _oilFieldService.Get(oilFieldSearchData: oilFieldSearchData, pagination: oilFieldSearchRequest.Pagination, sorts: oilFieldSearchRequest.Sorts);

            return Ok(result);
        }
    }
}