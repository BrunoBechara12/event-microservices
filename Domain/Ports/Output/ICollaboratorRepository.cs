using Domain.Entities;

namespace Domain.Ports.Output;
public interface ICollaboratorRepository
{
    Task<IEnumerable<EventCollaborator>> GetByEventId(int eventId);
    Task<Collaborator> Create(Collaborator collaborator, EventCollaborator eventCollaborator);
    Task<EventCollaborator> UpdateRole(EventCollaborator collaborator);
    Task<Collaborator> UpdateCollaborator(Collaborator collaborator);
    Task<EventCollaborator> Remove(int collaboratorId, int eventId);
}
