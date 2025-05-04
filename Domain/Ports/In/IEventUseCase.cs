using Domain.Entities;

namespace Domain.Ports.In;
public interface IEventUseCase
{
    Task<IEnumerable<Event>> GetAllAsync();
}
