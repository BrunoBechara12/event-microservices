
using Domain.Entities;
using t;

namespace Domain.Ports.In;
public interface ICollaboratorUseCase
{
    Task<Result<IEnumerable<Collaborator>>> Get(int userId);
    Task<Result<Collaborator>> Create(Collaborator collaborator, int eventId);
    Task<Result<Collaborator>> UpdateRole(Collaborator collaborator);
    Task<Result<Collaborator>> Remove(int collaboratorId, int eventId);
}
