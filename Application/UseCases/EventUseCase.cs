using Application.Dto;
using Domain.Entities;
using Domain.Ports.In;
using Domain.Ports.Output;
using t;

namespace Application.UseCases;
public class EventUseCase : IEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public EventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<IEnumerable<Event>>> GetAll()
    {
        var events = await _eventRepository.GetAll();

        if (events == null || !events.Any())
        {
            return Result<IEnumerable<Event>>.Success(events, "No events found.");
        }

        return Result<IEnumerable<Event>>.Success(events, "Events found with sucess!");
    }
    public async Task<Result<Event>> GetById(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        if(eventItem == null)
        {
            return Result<Event>.Success(eventItem, "Event not found.");
        }

        return Result<Event>.Success(eventItem, "Event found with sucess!");
    }
    public async Task<Result<IEnumerable<Event>>> GetByUserId(int userId)
    {
        var eventItems = await _eventRepository.GetByUserId(userId);

        if(eventItems == null || !eventItems.Any())
        {
            return Result<IEnumerable<Event>>.Success(eventItems, "No events found for this user.");
        }

        return Result<IEnumerable<Event>>.Success(eventItems, "Events found with sucess!"); 
    }

    public async Task<Result<Event>> Create(Event createEvent)
    {

        var createdEvent = await _eventRepository.Create(createEvent);

        return Result<Event>.Success(createdEvent, "Event created with success!");
        
    }

    public async Task<Result<Event>> Update(Event updateEvent)
    {
        var updatedEvent = await _eventRepository.Update(updateEvent);

        if (updatedEvent == null)
        {
            return Result<Event>.Failure("Event not found.");
        }

        return Result<Event>.Success(updatedEvent, "Event updated with success!");
    }

    public async Task<Result<Event>> Delete(int id)
    {
        var deletedEvent = await _eventRepository.Delete(id);

        if (deletedEvent == null)
        {
            return Result<Event>.Failure("Event not found.");
        }

        return Result<Event>.Success(null, "Event deleted with success!");
    }
}
