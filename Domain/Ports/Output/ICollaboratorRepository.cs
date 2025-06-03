using Domain.Entities;

namespace Domain.Ports.Output;
public interface ICollaboratorRepository
{
    Task<IEnumerable<Collaborator>> Get(int eventId);
    Task<Collaborator> Create(Collaborator collaborator, int eventId);
    Task<Collaborator> UpdateRole(Collaborator collaborator);
    Task<Collaborator> Remove(int collaboratorId, int eventId);
}
