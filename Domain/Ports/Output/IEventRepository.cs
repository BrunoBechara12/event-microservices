using Domain.Entities;

namespace Domain.Ports.Output;
public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAll();
}
