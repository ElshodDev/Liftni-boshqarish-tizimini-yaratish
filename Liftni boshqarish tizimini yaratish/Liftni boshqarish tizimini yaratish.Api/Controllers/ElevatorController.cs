using Liftni_boshqarish_tizimini_yaratish.Api.Controllers.Services;
using Liftni_boshqarish_tizimini_yaratish.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Liftni_boshqarish_tizimini_yaratish.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly ElevatorService _elevatorService;

        public ElevatorController(ElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var status = _elevatorService.GetCurrentStatus();
            return Ok(status);
        }

        [HttpGet("requests")]
        public IActionResult GetRequests()
        {
            var requests = _elevatorService.GetRequests();
            return Ok(requests);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MoveToFloor([FromQuery] int floor)
        {
            var message = await _elevatorService.RequestElevatorAsync(floor);
            return Ok(new { message });
        }

        // ⬇️ CRUD endpointlar
        [HttpGet("request/{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var request = await _elevatorService.GetRequestByIdAsync(id);
            return request == null ? NotFound() : Ok(request);
        }

        [HttpPost("request")]
        public async Task<IActionResult> CreateRequest([FromBody] FloorRequest request)
        {
            var created = await _elevatorService.CreateRequestAsync(request);
            return CreatedAtAction(nameof(GetRequest), new { id = created.Id }, created);
        }

        [HttpPut("request/{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] FloorRequest request)
        {
            var updated = await _elevatorService.UpdateRequestAsync(id, request);
            return updated ? Ok("Yangilandi") : NotFound();
        }

        [HttpDelete("request/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var deleted = await _elevatorService.DeleteRequestAsync(id);
            return deleted ? Ok("O'chirildi") : NotFound();
        }
    }
}
