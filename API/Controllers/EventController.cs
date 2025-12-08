using API.Dto.EventDto;
using API.Mappers.EventMapper;
using Domain.Ports.In;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        var domainEvents = await _eventUseCase.GetAll();

        if (domainEvents.RequestSuccess == true)
        {
            var dtoEvents = domainEvents.Data!.Select(ReturnEventMapper.ToDto).ToList();

            return Ok(new
            {
                message = domainEvents.Message,
                data = dtoEvents
            });
        }

        return BadRequest(new { message = domainEvents.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var domainEvent = await _eventUseCase.GetById(id);

        if (domainEvent.RequestSuccess == true)
        {
            var dtoEvent = ReturnEventMapper.ToDto(domainEvent.Data!);

            return Ok(new
            {
                message = domainEvent.Message,
                data = dtoEvent
            });
        }

        return BadRequest(new { message = domainEvent.Message });
    }

    [HttpGet("~/api/users/{userId}/events")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var domainEvents = await _eventUseCase.GetByUserId(userId);

        if (domainEvents.RequestSuccess == true)
        {
            var dtoEvents = domainEvents.Data!.Select(ReturnEventMapper.ToDto).ToList();

            return Ok(new
            {
                message = domainEvents.Message,
                data = dtoEvents
            });
        }

        return BadRequest(new { message = domainEvents.Message });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventDto createEvent)
    {
        var domainEvent = CreateEventMapper.ToDomain(createEvent);

        var eventItem = await _eventUseCase.Create(domainEvent);

        if (eventItem.RequestSuccess == true)
        {
            var dtoEvent = ReturnEventCreatedMapper.ToDto(eventItem.Data!);

            return Ok(new
            {
                message = eventItem.Message,
                data = dtoEvent
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEventDto updateEvent)
    {
        var domainEvent = UpdateEventMapper.ToDomain(updateEvent);

        var eventItem = await _eventUseCase.Update(domainEvent);

        if (eventItem.RequestSuccess == true)
        {
            var dtoEvent = ReturnEventMapper.ToDto(eventItem.Data!);

            return Ok(new
            {
                message = eventItem.Message,
                data = dtoEvent
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var deletedEvent = await _eventUseCase.Delete(id);

         if (deletedEvent.RequestSuccess == true)
         {
            return Ok(new
            {
                message = deletedEvent.Message,
                data = deletedEvent.Data
            });
         }

         return BadRequest(new { message = deletedEvent.Message });
    }


}
