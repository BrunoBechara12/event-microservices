using Domain.Entities;
using Result;

namespace Domain.Ports.In;
public interface IEventUseCase
{
    Task<Result<IEnumerable<Event>>> GetAll();
    Task<Result<Event>> GetById(int id);
    Task<Result<IEnumerable<Event>>> GetByUserId(int userId);
    Task<Result<Event>> Create(Event createEvent);
    Task<Result<Event>> Update(Event updateEvent);
    Task<Result<Event>> Delete(int id);
}
