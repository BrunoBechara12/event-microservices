using Domain.Entities;
using Result;

namespace Domain.Ports.In;
public interface ICollaboratorUseCase
{
    Task<Result<IEnumerable<EventCollaborator>>> GetByEventId(int eventId);
    Task<Result<Collaborator>> Create(Collaborator collaborator, EventCollaborator eventCollaborator);
    Task<Result<EventCollaborator>> UpdateRole(EventCollaborator collaborator);
    Task<Result<Collaborator>> UpdateCollaborator(Collaborator collaborator);
    Task<Result<Collaborator>> Remove(int collaboratorId, int eventId);
}
