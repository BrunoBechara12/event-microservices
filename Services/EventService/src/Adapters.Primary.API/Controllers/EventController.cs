using Application.UseCases.Event.Inputs;
using Microsoft.AspNetCore.Mvc;
using Application.Ports.In;

namespace Adapters.Primary.API.Controllers;

[Route("api/events")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventUseCase _eventUseCase;

    public EventController(IEventUseCase eventUseCase)
    {
        _eventUseCase = eventUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var events = await _eventUseCase.Get();

        if (events.RequestSuccess)
        {
            return Ok(new
            {
                message = events.Message,
                data = events.Data
            });
        }

        return BadRequest(new { message = events.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var eventItem = await _eventUseCase.GetById(id);

        if (eventItem.RequestSuccess)
        {
            return Ok(new
            {
                message = eventItem.Message,
                data = eventItem.Data
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpGet("~/api/users/{userId}/events")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var events = await _eventUseCase.GetByUserId(userId);

        if (events.RequestSuccess)
        {
            return Ok(new
            {
                message = events.Message,
                data = events.Data
            });
        }

        return BadRequest(new { message = events.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventInput input)
    {
        var eventItem = await _eventUseCase.Create(input);

        if (eventItem.RequestSuccess)
        {
            return Ok(new
            {
                message = eventItem.Message,
                data = eventItem.Data
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEventInput updateEvent)
    {
        var eventItem = await _eventUseCase.Update(updateEvent);

        if (eventItem.RequestSuccess)
        {
            return Ok(new
            {
                message = eventItem.Message,
                data = eventItem.Data
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var eventItem = await _eventUseCase.Delete(id);

         if (eventItem.RequestSuccess)
         {
            return Ok(new
            {
                message = eventItem.Message,
                data = eventItem.Data
            });
         }

         return BadRequest(new { message = eventItem.Message });
    }
}
