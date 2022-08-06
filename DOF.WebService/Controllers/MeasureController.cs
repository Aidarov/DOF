using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOF.WebService.Controllers.Mappings;
using DOF.WebService.Controllers.Request.Measure;
using DOF.WebService.Services.Device;
using DOF.WebService.Services.Measure;
using DOF.WebService.Services.User;
using DOF.WebService.Services.UserSession;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOF.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : MainController
    {
        private readonly IMeasureService _measureService;
        public MeasureController(IMeasureService measureService, IUserService userService,
            IUserSessionService userSessionService,
            IDeviceService deviceService) : base(userService, userSessionService, deviceService)
        {
            _measureService = measureService;
        }

        [HttpPost("AddRange")]
        public async Task<ActionResult> AddRange(IEnumerable<MeasureAddRequest> measureAddRequests)
        {
            if (!ModelState.IsValid) return BadRequest();

            var measures = new List<MeasureAddData>();

            var deviceId = await GetDeviceId();

            foreach (var measureAddRequest in measureAddRequests)
            {
                var createdBy = 0;                
                measures.Add(measureAddRequest.MapToMeasureAddData(acceptedAt: DateTime.Now, deviceId: deviceId, createdBy: createdBy));
            }
                

            await _measureService.AddRange(measures);

            return Ok();
        }

        [HttpGet("Get")]
        public async Task<ActionResult> Get(MeasureSearchRequest measureSearchRequest)
        {
            var filter = measureSearchRequest.MapToMeasureSearchData();

            var result = await _measureService.Get(filter: filter, pagination: measureSearchRequest.Pagination, sorts: measureSearchRequest.Sorts);

            return Ok(result);
        }

    }
}