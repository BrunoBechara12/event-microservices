using Domain.Entities;
using static Domain.Entities.Collaborator;

namespace Domain.Ports.Output;
public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAll();
    Task<Event> GetById(int id);
    Task<IEnumerable<Event>> GetByUserId(int userId);
    Task<Event> Create(Event createEvent);
    Task<Event> Update(Event updateEvent);
    Task<Event> Delete(int id);
    Task<IEnumerable<Collaborator>> GetCollaborators(int eventId);
    Task AddCollaborator(int eventId, int userId, CollaboratorRole role);
    Task UpdateCollaboratorRole(int eventId, int userId, CollaboratorRole role);
    Task RemoveCollaborator(int eventId, int userId);
}
