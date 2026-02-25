using Domain.DTOs.Notification.Requests;
using Domain.Ports.Input;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Primary.API.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationUseCase _notificationUseCase;

    public NotificationsController(INotificationUseCase notificationUseCase)
    {
        _notificationUseCase = notificationUseCase;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _notificationUseCase.GetById(id);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequestDto request)
    {
        var result = await _notificationUseCase.SendNotification(request);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpPost("templated")]
    public async Task<IActionResult> SendTemplatedNotification([FromBody] SendTemplatedNotificationRequestDto request)
    {
        var result = await _notificationUseCase.SendTemplatedNotification(request);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpPost("{id}/resend")]
    public async Task<IActionResult> ResendNotification(int id)
    {
        var result = await _notificationUseCase.ResendFailedNotification(id);

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }

    [HttpGet("whatsapp/status")]
    public async Task<IActionResult> GetWhatsAppStatus()
    {
        var result = await _notificationUseCase.GetWhatsAppStatus();

        if (result.RequestSuccess)
        {
            return Ok(new
            {
                message = result.Message,
                data = result.Data
            });
        }

        return BadRequest(new { message = result.Message });
    }
}
