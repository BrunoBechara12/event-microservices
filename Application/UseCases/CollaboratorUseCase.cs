using Domain.Entities;
using Domain.Ports.In;
using Domain.Ports.Output;
using t;

namespace Application.UseCases;
public class CollaboratorUseCase : ICollaboratorUseCase
{
    public readonly ICollaboratorRepository _collaboratorRepository;
    public readonly IEventRepository _eventRepository;

    public CollaboratorUseCase(ICollaboratorRepository collaboratorRepository, IEventRepository eventRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Result<IEnumerable<Collaborator>>> GetByEventId(int eventId)
    {
        var collaborators = await _collaboratorRepository.GetByEventId(eventId);

        if (collaborators == null)
        {
            return Result<IEnumerable<Collaborator>>.Success(collaborators, "No collaborators found.");
        }

        return Result<IEnumerable<Collaborator>>.Success(collaborators, "Collaborators found with success!");
    }

    public async Task<Result<Collaborator>> Create(Collaborator collaborator, EventCollaborator eventCollaborator)
    {
        var createdCollaborator = await _collaboratorRepository.Create(collaborator, eventCollaborator);

        return Result<Collaborator>.Success(createdCollaborator, "Collaborator created with success!");
    }

    public async Task<Result<Collaborator>> Remove(int collaboratorId, int eventId)
    {
        var eventItem = await _eventRepository.GetById(eventId);
        if(eventItem == null)
            return Result<Collaborator>.Failure("Event not found.");
        
        var eventCollaboratorRemoved = await _collaboratorRepository.Remove(collaboratorId, eventId);

        if(eventCollaboratorRemoved == null)
            return Result<Collaborator>.Failure("Collaborator is not in this event.");

        return Result<Collaborator>.Success(null, "Collaborator removed with success!");
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
