using Application.Dto;
using Application.Mappers;
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
        var events = await _eventUseCase.GetAll();

        if (events.RequestSuccess == true)
            return Ok(new
            {
                message = events.Message,
                data = events.Value
            });

        return BadRequest(new { message = events.Message });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var eventItem = await _eventUseCase.GetById(id);

        if (eventItem.RequestSuccess == true)
            return Ok(new
            {
                message = eventItem.Message,
                data = eventItem.Value
            });

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpGet("~/api/users/{userId}/events")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var events = await _eventUseCase.GetByUserId(userId);

        if (events.RequestSuccess == true)
            return Ok(new
            {
                message = events.Message,
                data = events.Value
            });

        return BadRequest(new { message = events.Message });
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateEventDto createEvent)
    {
        var domainEvent = CreateEventMapper.ToDomain(createEvent);

        var eventItem = await _eventUseCase.Create(domainEvent);

        if (eventItem.RequestSuccess == true)
        {
            var returnEventCreated = ReturnEventCreatedMapper.ToDto(eventItem.Value);

            return Ok(new
            {
                message = eventItem.Message,
                data = returnEventCreated
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateEventDto updateEvent)
    {
        var domainEvent = UpdateEventMapper.ToDomain(updateEvent);

        var eventItem = await _eventUseCase.Update(domainEvent);

        if (eventItem.RequestSuccess == true)
        {
            var returnEventUpdated = ReturnEventUpdatedMapper.ToDto(eventItem.Value);

            return Ok(new
            {
                message = eventItem.Message,
                data = returnEventUpdated
            });
        }

        return BadRequest(new { message = eventItem.Message });
    }


}
