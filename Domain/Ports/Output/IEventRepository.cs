using Domain.Entities;

namespace Domain.Ports.Output;
public interface IEventRepository
{
    Task<IEnumerable<Event>> Get();
    Task<Event?> GetById(int id);
    Task<IEnumerable<Event>> GetByUserId(int userId);
    Task<Event> Create(Event createEvent);
    Task Update(Event updateEvent);
    Task Delete(Event eventItem);
}
