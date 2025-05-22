using Domain.Entities;
using t;

namespace Domain.Ports.In;
public interface IEventUseCase
{
    Task<Result<IEnumerable<Event>>> GetAll();
    Task<Result<Event>> GetById(int id);
    Task<Result<IEnumerable<Event>>> GetByUserId(int userId);
    Task<Result<Event>> Create(Event createEvent);
    Task<Result<Event>> Update(Event updateEvent);
    Task<Result<Event>> Delete(int id);
    Task<Result<IEnumerable<Event>>> GetCollaborators(int userId);
    Task AddCollaborator(int eventId, int userId);
    Task UpdateCollaboratorRole(int userId);
    Task RemoveCollaborator(int eventId, int userId);
}
