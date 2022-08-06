using System;
using System.Threading.Tasks;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.OilWell;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.OilWell;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilWellController : MainController
    {
        private readonly IOilWellService _oilWellService;

        public OilWellController(IOilWellService oilWellService, IUserService userService,
            IUserSessionService userSessionService,
            IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _oilWellService = oilWellService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(OilWellAddRequest oilWellAddRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentUserId = await GetUserId();

            var oilWellAddData = oilWellAddRequest.MapToOilWellAddData(createdBy: currentUserId, createdAt: DateTime.Now, isDeleted: false);

            var result = await _oilWellService.Add(oilWellAddData);

            return Ok();
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(OilWellUpdateRequest oilWellUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var oilWellUpdateData = oilWellUpdateRequest.MapToOilWellUpdateData();

            await _oilWellService.Update(oilWellUpdateData);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var currentUserId = await GetUserId();

            await _oilWellService.Delete(id: id, deletedBy: currentUserId, deletedAt: DateTime.Now);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _oilWellService.GetById(id);

            return Ok(result);
        }

        [HttpPost("Get")]
        public async Task<ActionResult> Get(OilWellSearchRequest oilWellSearchRequest)
        {
            var oilWellSearchData = oilWellSearchRequest.MapToOilFieldSearchData();

            var result = await _oilWellService.Get(oilWellSearchData: oilWellSearchData, pagination: oilWellSearchRequest.Pagination, sorts: oilWellSearchRequest.Sorts);

            return Ok(result);
        }
    }
}