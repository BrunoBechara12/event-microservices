using Domain.Contracts.Collaborator.Inputs;
using Domain.Contracts.Collaborator.Outputs;
using Result;

namespace Domain.Ports.In;
public interface ICollaboratorUseCase
{
    Task<Result<DetailedCollaboratorOutput>> GetById(int id);
    Task<Result<IEnumerable<DetailedCollaboratorOutput>>> GetByEventId(int eventId);
    Task<Result<DefaultCollaboratorOutput>> Create(CreateCollaboratorInput input);
    Task<Result<DefaultCollaboratorOutput>> Update(UpdateCollaboratorInput input);
    Task<Result<DefaultCollaboratorOutput>> Delete(int id);
    Task<Result<DefaultCollaboratorOutput>> AddToEvent(AddCollaboratorToEventInput input);
    Task<Result<DefaultCollaboratorOutput>> RemoveFromEvent(RemoveCollaboratorFromEventInput input);
}
