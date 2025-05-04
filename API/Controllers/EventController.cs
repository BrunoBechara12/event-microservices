using Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;
[Route("api/event")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventUseCase _eventUseCase;

    public EventController(IEventUseCase eventUseCase)
    {
        _eventUseCase = eventUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventUseCase.GetAllAsync();
        return Ok(events);
    }
}
