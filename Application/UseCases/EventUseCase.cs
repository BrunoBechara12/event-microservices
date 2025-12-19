using Application.UseCases.Event.Inputs;
using Application.UseCases.Event.Outputs;
using Domain.Ports.Output;
using Result;
using Application.Ports.In;
using static Domain.Entities.Event;

namespace Application.UseCases;
public class EventUseCase : IEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public EventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Result<IEnumerable<DetailedEventOutput>>> Get()
    {
        var events = await _eventRepository.Get();

        var eventOutput = events.Select(e => e.ToDetailedEventOutput());

        if(!eventOutput.Any())
        {
            return Result<IEnumerable<DetailedEventOutput>>.Success(eventOutput, "No events found.");
        }

        return Result<IEnumerable<DetailedEventOutput>>.Success(eventOutput, "Events found with success!");
    }
    public async Task<Result<DetailedEventOutput>> GetById(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        var eventOutput = eventItem?.ToDetailedEventOutput();

        if (eventItem == null)
        {
            return Result<DetailedEventOutput>.Success(eventOutput, "Event not found.");
        }

        return Result<DetailedEventOutput>.Success(eventOutput, "Event found with success!");
    }
    public async Task<Result<IEnumerable<DetailedEventOutput>>> GetByUserId(int userId)
    {
        var eventItems = await _eventRepository.GetByUserId(userId);

        var eventOutput = eventItems.Select(e => e.ToDetailedEventOutput());

        if (!eventItems.Any())
        {
            return Result<IEnumerable<DetailedEventOutput>>.Success(eventOutput, "No events found for this user.");
        }

        return Result<IEnumerable<DetailedEventOutput>>.Success(eventOutput!, "Events found with success!"); 
    }

    public async Task<Result<DefaultEventOutput>> Create(CreateEventInput input)
    {
        var newEvent = CreateEvent( 
            input.Name, 
            input.Description, 
            input.Location, 
            input.StartDate, 
            input.OwnerUserId
        );

        var createdEvent = await _eventRepository.Create(newEvent);

        var eventOutput = createdEvent.ToDefaultEventOutput();

        return Result<DefaultEventOutput>.Success(eventOutput, "Event created with success!");
    }

    public async Task<Result<DefaultEventOutput>> Update(UpdateEventInput updateEvent)
    {
        var eventItem = await _eventRepository.GetById(updateEvent.Id);

        if(eventItem == null)
        {
            return Result<DefaultEventOutput>.Failure("Event not found.");
        }

        eventItem.UpdateEvent(
            updateEvent.Name, 
            updateEvent.Description,
            updateEvent.OwnerUserId,
            updateEvent.Location,
            updateEvent.StartDate
        );

        await _eventRepository.Update(eventItem);

        var eventOutput = eventItem.ToDefaultEventOutput();

        return Result<DefaultEventOutput>.Success(eventOutput, "Event updated with success!");
    }

    public async Task<Result<DefaultEventOutput>> Delete(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        if(eventItem == null)
        {
            return Result<DefaultEventOutput>.Failure("Event not found.");
        }

        await _eventRepository.Delete(eventItem);

        return Result<DefaultEventOutput>.Success(null, "Event deleted with success!");
    }
}
