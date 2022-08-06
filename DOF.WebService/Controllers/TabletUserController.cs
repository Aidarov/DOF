using System;
using System.Threading.Tasks;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.TabletUser;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.TabletUser;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabletUserController : MainController
    {
        private readonly ITabletUserService _tabletUserService;
        public TabletUserController(ITabletUserService tabletUserService, IUserService userService,
            IUserSessionService userSessionService,
            IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _tabletUserService = tabletUserService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(TabletUserAddRequest tabletUserAddRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentUserId = await GetUserId();

            var tabletUserAddData = tabletUserAddRequest.MapToTabletUserAddData(createdBy: currentUserId, createdAt: DateTime.Now, isDeleted: false);

            var result = await _tabletUserService.Add(tabletUserAddData);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(TabletUserUpdateRequest tabletUserUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var tabletUserUpdateData = tabletUserUpdateRequest.MapToTabletUserUpdateData();

            await _tabletUserService.Update(tabletUserUpdateData);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var currentUserId = await GetUserId();

            await _tabletUserService.Delete(id: id, deletedBy: currentUserId, deletedAt: DateTime.Now);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _tabletUserService.GetById(id);

            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<ActionResult> Get(TabletUserSearchRequest tabletUserSearchRequest)
        {
            var tabletUserSearchData = tabletUserSearchRequest.MapToTabletUserSearchData();

            var result = await _tabletUserService.Get(filter: tabletUserSearchData, pagination: tabletUserSearchRequest.Pagination, sorts: tabletUserSearchRequest.Sorts);

            return Ok(result);
        }
    }
}