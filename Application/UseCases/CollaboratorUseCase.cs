using Domain.Entities;
using Domain.Ports.In;
using Domain.Ports.Output;
using t;

namespace Application.UseCases;
public class CollaboratorUseCase : ICollaboratorUseCase
{
    public readonly ICollaboratorRepository _collaboratorRepository;

    public CollaboratorUseCase(ICollaboratorRepository collaboratorRepository)
    {
        _collaboratorRepository = collaboratorRepository;
    }

    public async Task<Result<Collaborator>> Create(Collaborator collaborator, EventCollaborator eventCollaborator)
    {
        var createdCollaborator = await _collaboratorRepository.Create(collaborator, eventCollaborator);

        return Result<Collaborator>.Success(createdCollaborator, "Collaborator created with success!");
    }

    public Task<Result<IEnumerable<Collaborator>>> Get(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Collaborator>> Remove(int collaboratorId, int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Collaborator>> UpdateRole(Collaborator collaborator)
    {
        throw new NotImplementedException();
    }
}
