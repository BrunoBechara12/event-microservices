using Domain.Entities;

namespace Domain.Ports.Output;
public interface ICollaboratorRepository
{
    Task<IEnumerable<Collaborator>> GetByEventId(int eventId);
    Task<Collaborator> Create(Collaborator collaborator, EventCollaborator eventCollaborator);
    Task<EventCollaborator> UpdateRole(EventCollaborator collaborator);
    Task<EventCollaborator> Remove(int collaboratorId, int eventId);
}
