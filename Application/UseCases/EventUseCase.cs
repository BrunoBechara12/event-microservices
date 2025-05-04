using Domain.Entities;
using Domain.Ports.In;
using Domain.Ports.Output;

namespace Application.UseCases;
public class EventUseCase : IEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public EventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        var events = await _eventRepository.GetAll();
        return events;
    }
}
