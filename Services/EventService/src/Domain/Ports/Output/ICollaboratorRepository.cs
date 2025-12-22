using Domain.Entities;

namespace Domain.Ports.Output;

public interface ICollaboratorRepository
{
    Task<Collaborator?> GetById(int id);
    Task<IEnumerable<EventCollaborator>> GetByEventId(int eventId);
    Task<Collaborator> Create(Collaborator collaborator);
    Task Update(Collaborator collaborator);
    Task<bool> IsCollaboratorInEvent(int collaboratorId, int eventId);
    Task AddToEvent(EventCollaborator eventCollaborator);
    Task RemoveFromEvent(int collaboratorId, int eventId);
}