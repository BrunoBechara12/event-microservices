using Domain.DTOs.Event.Requests;
using Domain.DTOs.Event.Responses;
using Domain.Mappers;
using Domain.Events;
using Domain.Ports.Input;
using Domain.Ports.Output;
using Result;
using static Domain.Entities.Event;

namespace Application.UseCases.Event;
public class EventUseCase : IEventUseCase
{
    private readonly IEventRepository _eventRepository;
    public readonly IMessagePublisher _messagePublisher;

    public EventUseCase(IEventRepository eventRepository, IMessagePublisher messagePublisher)
    {
        _eventRepository = eventRepository;
        _messagePublisher = messagePublisher;
    }

    public async Task<Result<IEnumerable<DetailedEventResponseDto>>> Get()
    {
        var events = await _eventRepository.Get();

        var eventOutput = events.Select(e => e.ToDetailedResponseDto());

        if(!eventOutput.Any())
        {
            return Result<IEnumerable<DetailedEventResponseDto>>.Success(eventOutput, "No events found.");
        }

        return Result<IEnumerable<DetailedEventResponseDto>>.Success(eventOutput, "Events found with success!");
    }
    public async Task<Result<DetailedEventResponseDto>> GetById(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        var eventOutput = eventItem?.ToDetailedResponseDto();

        if (eventItem == null)
        {
            return Result<DetailedEventResponseDto>.Success(eventOutput, "Event not found.");
        }

        return Result<DetailedEventResponseDto>.Success(eventOutput, "Event found with success!");
    }
    public async Task<Result<IEnumerable<DetailedEventResponseDto>>> GetByUserId(int userId)
    {
        var eventItems = await _eventRepository.GetByUserId(userId);

        var eventOutput = eventItems.Select(e => e.ToDetailedResponseDto());

        if (!eventItems.Any())
        {
            return Result<IEnumerable<DetailedEventResponseDto>>.Success(eventOutput, "No events found for this user.");
        }

        return Result<IEnumerable<DetailedEventResponseDto>>.Success(eventOutput!, "Events found with success!"); 
    }

    public async Task<Result<DefaultEventResponseDto>> Create(CreateEventRequestDto input)
    {
        var newEvent = CreateEvent( 
            input.Name, 
            input.Description, 
            input.Location, 
            input.StartDate, 
            input.OwnerUserId
        );

        var createdEvent = await _eventRepository.Create(newEvent);

        var eventOutput = createdEvent.ToDefaultResponseDto();

        await _messagePublisher.PublishAsync<EventCreated>(new EventCreated(eventOutput!.Id, eventOutput.Name), CancellationToken.None);

        return Result<DefaultEventResponseDto>.Success(eventOutput, "Event created with success!");
    }

    public async Task<Result<DefaultEventResponseDto>> Update(UpdateEventRequestDto updateEvent)
    {
        var eventItem = await _eventRepository.GetById(updateEvent.Id);

        if(eventItem == null)
        {
            return Result<DefaultEventResponseDto>.Failure("Event not found.");
        }

        eventItem.UpdateEvent(
            updateEvent.Name, 
            updateEvent.Description,
            updateEvent.OwnerUserId,
            updateEvent.Location,
            updateEvent.StartDate
        );

        await _eventRepository.Update(eventItem);

        var eventOutput = eventItem.ToDefaultResponseDto();

        return Result<DefaultEventResponseDto>.Success(eventOutput, "Event updated with success!");
    }

    public async Task<Result<DefaultEventResponseDto>> Delete(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        if(eventItem == null)
        {
            return Result<DefaultEventResponseDto>.Failure("Event not found.");
        }

        await _eventRepository.Delete(eventItem);

        await _messagePublisher.PublishAsync<EventDeleted>(new EventDeleted(id), CancellationToken.None);

        return Result<DefaultEventResponseDto>.Success(null, "Event deleted with success!");
    }
}
