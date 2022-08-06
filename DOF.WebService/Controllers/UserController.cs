using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.User;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IUserSessionService userSessionService, IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _userService = userService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(UserAddRequest userAddRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentUserId = await GetUserId();

            var userAddData = userAddRequest.MapToUserAddData(createdBy: currentUserId, createdAt: DateTime.Now, isDeleted: false);

            var result = await _userService.Add(userAddData);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UserUpdateRequest userUpdateRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var userUpdateData = userUpdateRequest.MapToUserUpdateData();

            await _userService.Update(userUpdateData);

            return Ok();
        }

        [HttpPut("SetPassword")]
        public async Task<ActionResult> SetPassword(UserPasswordSetRequest userPasswordSetRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _userService.SetNewPassword(new UserPasswordSetData() 
            {
                UserId = userPasswordSetRequest.UserId,
                NewPassword = userPasswordSetRequest.NewPassword,
                ConfirmPassword = userPasswordSetRequest.ConfirmPassword
            });

            return Ok();
        }

        [HttpPut("UpdatePassword")]
        public async Task<ActionResult> UpdatePassword(UserPasswordUpdateRequest userPasswordSetRequest)
        {
            await _userService.UpdatePassword(new UserPasswordUpdateData()
            {
                UserId = userPasswordSetRequest.UserId,
                ConfirmPassword = userPasswordSetRequest.ConfirmPassword,
                CurrentPassword = userPasswordSetRequest.CurrentPassword,
                NewPassword = userPasswordSetRequest.NewPassword
            });

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.Block(id);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);

            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<ActionResult> Get(UserSearchRequest userSearchRequest)
        {
            var userSearchData = userSearchRequest.MapToUserSearchData();

            var result = await _userService.Get(userSearchData: userSearchData, pagination: userSearchRequest.Pagination, sorts: userSearchRequest.Sorts);

            return Ok(result);
        }

        [HttpPost("Authorize")]
        public async Task<ActionResult> Authorize(UserAuthorizeRequest userAuthorizeRequest)
        {
            var result = await _userService.Authorize(new UserAuthorizeData()
            {
                Login = userAuthorizeRequest.Login,
                Password = userAuthorizeRequest.Password,
                IpAddress = IpAddress
            });

            return Ok(result);
        }
    }
}