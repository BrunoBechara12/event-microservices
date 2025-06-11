using Domain.Entities;

namespace Domain.Ports.Output;
public interface ICollaboratorRepository
{
    Task<IEnumerable<Collaborator>> Get(int eventId);
    Task<Collaborator> Create(Collaborator collaborator, EventCollaborator eventCollaborator);
    Task<EventCollaborator> UpdateRole(EventCollaborator collaborator);
    Task<Collaborator> Remove(int collaboratorId, int eventId);
}
