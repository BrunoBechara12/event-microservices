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

    public async Task<Result<EventCollaborator>> UpdateRole(EventCollaborator eventCollaborator)
    {
        var collaboratorItem = await _collaboratorRepository.UpdateRole(eventCollaborator);

        if (collaboratorItem == null)
        {
            return Result<EventCollaborator>.Failure("Collaborator not found.");
        }

        return Result<EventCollaborator>.Success(collaboratorItem, "Collaborator role updated with success!");
    }
}
