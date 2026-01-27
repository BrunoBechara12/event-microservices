using Domain.Entities;

namespace Domain.Ports.Output;

public interface IEventRepository
{
    Task<List<Event>?> Get();
    Task<Event?> GetById(int id);
    Task<Event> Create(Event eventEntity);
    Task Update(Event eventEntity);
    Task Delete(Event eventEntity);
}
