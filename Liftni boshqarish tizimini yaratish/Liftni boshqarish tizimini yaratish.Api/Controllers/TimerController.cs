using Liftni_boshqarish_tizimini_yaratish.Api.Controllers.Services;
using Liftni_boshqarish_tizimini_yaratish.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Liftni_boshqarish_tizimini_yaratish.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimerController : ControllerBase
{
    private readonly TimerService _timerService;

    public TimerController(TimerService timerService)
    {
        _timerService = timerService;
    }

    [HttpPost("start")]
    public IActionResult Start([FromQuery] int seconds)
    {
        var result = _timerService.StartTimer(seconds);
        return Ok(new { message = result });
    }

    [HttpPost("stop")]
    public IActionResult Stop()
    {
        var result = _timerService.StopTimer();
        return Ok(new { message = result });
    }

    [HttpGet("current")]
    public IActionResult GetCurrent()
    {
        var result = _timerService.GetCurrent();
        return Ok(result);
    }

    [HttpGet("history")]
    public IActionResult History()
    {
        var result = _timerService.GetHistory();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _timerService.GetById(id);
        if (result == null)
            return NotFound(new { message = "Session topilmadi" });

        return Ok(result);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TimerSession updated)
    {
        var message = _timerService.Update(id, updated);
        if (message.Contains("topilmadi"))
            return NotFound(new { message });

        return Ok(new { message });
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var message = _timerService.Delete(id);
        if (message.Contains("topilmadi"))
            return NotFound(new { message });

        return Ok(new { message });
    }
}
