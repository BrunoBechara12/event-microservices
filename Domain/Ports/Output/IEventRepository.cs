using Domain.Entities;

namespace Domain.Ports.Output;
public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAll();
    Task<Event?> GetById(int id);
    Task<IEnumerable<Event>> GetByUserId(int userId);
    Task<Event> Create(Event createEvent);
    Task<Event> Update(Event updateEvent);
    Task<Event> Delete(int id);
}
