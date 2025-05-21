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
        try
        {
            var events = await _eventRepository.GetAll();

            if (events == null || !events.Any())
            {
                return Result<IEnumerable<Event>>.Success(events, "No events found.");
            }

            return Result<IEnumerable<Event>>.Success(events, "Events found with sucess!");

        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Event>>.Failure(ex.Message);
        }
    }
    public async Task<Result<Event>> GetById(int id)
    {
        try
        {
            var eventItem = await _eventRepository.GetById(id);

            if(eventItem == null)
            {
                return Result<Event>.Success(eventItem, "Event not found.");
            }

            return Result<Event>.Success(eventItem, "Event found with sucess!");
        }
        catch (Exception ex)
        {
            return Result<Event>.Failure(ex.Message);
        }
    }
    public async Task<Result<IEnumerable<Event>>> GetByUserId(int userId)
    {
        try
        {
            var eventItems = await _eventRepository.GetByUserId(userId);

            if(eventItems == null || !eventItems.Any())
            {
                return Result<IEnumerable<Event>>.Success(eventItems, "No events found for this user.");
            }

            return Result<IEnumerable<Event>>.Success(eventItems, "Events found with sucess!");
        }
        catch(Exception ex)
        {
            return Result<IEnumerable<Event>>.Failure(ex.Message);
        }
    }

    public async Task<Result<Event>> Create(Event createEvent)
    {
        try
        {
            var createdEvent = await _eventRepository.Create(createEvent);

            return Result<Event>.Success(createdEvent, "Event created with success!");
        } 
        catch(Exception ex)
        {
            return Result<Event>.Failure(ex.Message);
        }
    }

    public async Task<Result<Event>> Update(Event updateEvent)
    {
        try
        {
            var updatedEvent = await _eventRepository.Update(updateEvent);

            if (updatedEvent == null)
            {
                return Result<Event>.Failure("Event not found.");
            }

            return Result<Event>.Success(updatedEvent, "Event updated with success!");
        } 
        catch (Exception ex)
        {
            return Result<Event>.Failure(ex.Message);
        }
    }

    public Task Delete (int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<Event>>> GetCollaborators(int userId)
    {
        throw new NotImplementedException();
    }

    public Task AddCollaborator(int eventId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCollaboratorRole(int userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCollaborator(int eventId, int userId)
    {
        throw new NotImplementedException();
    }
}
