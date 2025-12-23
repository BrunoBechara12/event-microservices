using Application.Ports.In;
using Application.UseCases.Collaborator.Inputs;
using Application.UseCases.Collaborator.Outputs;
using Domain.Entities;
using Domain.Ports.Output;
using Result;
using static Domain.Entities.Collaborator;

namespace Application.UseCases.Collaborator;

public class CollaboratorUseCase : ICollaboratorUseCase
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly IEventRepository _eventRepository;

    public CollaboratorUseCase(ICollaboratorRepository collaboratorRepository, IEventRepository eventRepository)
    {
        _collaboratorRepository = collaboratorRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Result<DetailedCollaboratorOutput>> GetById(int id)
    {
        var collaborator = await _collaboratorRepository.GetById(id);

        var collaboratorOutput = collaborator?.ToDetailedCollaboratorOutput();

        if (collaborator == null)
        {
            return Result<DetailedCollaboratorOutput>.Success(collaboratorOutput, "Collaborator not found.");
        }

        return Result<DetailedCollaboratorOutput>.Success(collaboratorOutput, "Collaborator found with success!");
    }

    public async Task<Result<IEnumerable<DetailedCollaboratorOutput>>> GetByEventId(int eventId)
    {
        var eventExists = await _eventRepository.GetById(eventId);
        if (eventExists == null)
        {
            return Result<IEnumerable<DetailedCollaboratorOutput>>.Failure("Event not found.");
        }

        var collaborators = await _collaboratorRepository.GetByEventId(eventId);

        var collaboratorOutput = collaborators.Select(c => c.Collaborator.ToDetailedCollaboratorOutput()!);

        if (!collaborators.Any())
        {
            return Result<IEnumerable<DetailedCollaboratorOutput>>.Success(collaboratorOutput, "No collaborators found for this event.");
        }

        return Result<IEnumerable<DetailedCollaboratorOutput>>.Success(collaboratorOutput, "Collaborators found with success!");
    }

    public async Task<Result<DefaultCollaboratorOutput>> Create(CreateCollaboratorInput input)
    {
        var newCollaborator = CreateCollaborator(input.UserId, input.Name);

        var createdCollaborator = await _collaboratorRepository.Create(newCollaborator);

        var collaboratorOutput = createdCollaborator.ToDefaultCollaboratorOutput();

        return Result<DefaultCollaboratorOutput>.Success(collaboratorOutput!, "Collaborator created with success!");
    }

    public async Task<Result<DefaultCollaboratorOutput>> Update(UpdateCollaboratorInput input)
    {
        var collaboratorItem = await _collaboratorRepository.GetById(input.Id);

        if (collaboratorItem == null)
        {
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator not found.");
        }

        collaboratorItem.UpdateCollaborator(input.UserId, input.Name);

        await _collaboratorRepository.Update(collaboratorItem);

        var collaboratorOutput = collaboratorItem.ToDefaultCollaboratorOutput();

        return Result<DefaultCollaboratorOutput>.Success(collaboratorOutput!, "Collaborator updated with success!");
    }

    public async Task<Result<DefaultCollaboratorOutput>> Delete(int id)
    {
        var collaborator = await _collaboratorRepository.GetById(id);

        if (collaborator == null)
        {
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator not found.");
        }

        await _collaboratorRepository.Delete(collaborator);

        return Result<DefaultCollaboratorOutput>.Success(null, "Collaborator deleted with success!");
    }

    public async Task<Result<DefaultCollaboratorOutput>> AddToEvent(AddCollaboratorToEventInput input)
    {
        var collaborator = await _collaboratorRepository.GetById(input.CollaboratorId);
        if (collaborator == null)
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator not found.");

        var eventItem = await _eventRepository.GetById(input.EventId);
        if (eventItem == null)
            return Result<DefaultCollaboratorOutput>.Failure("Event not found.");

        var isAlreadyLinked = await _collaboratorRepository.IsCollaboratorInEvent(input.CollaboratorId, input.EventId);
        if (isAlreadyLinked)
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator is already in this event.");

        var eventCollaborator = EventCollaborator.CreateEventCollaborator(input.EventId, input.CollaboratorId, input.Role);

        await _collaboratorRepository.AddToEvent(eventCollaborator);

        return Result<DefaultCollaboratorOutput>.Success(collaborator.ToDefaultCollaboratorOutput()!, "Collaborator added to event with success!");
    }

    public async Task<Result<DefaultCollaboratorOutput>> RemoveFromEvent(RemoveCollaboratorFromEventInput input)
    {
        var collaborator = await _collaboratorRepository.GetById(input.CollaboratorId);
        if (collaborator == null)
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator not found.");

        var eventItem = await _eventRepository.GetById(input.EventId);
        if (eventItem == null)
            return Result<DefaultCollaboratorOutput>.Failure("Event not found.");

        var isLinked = await _collaboratorRepository.IsCollaboratorInEvent(input.CollaboratorId, input.EventId);
        if (!isLinked)
            return Result<DefaultCollaboratorOutput>.Failure("Collaborator is not linked to this event.");

        await _collaboratorRepository.RemoveFromEvent(input.CollaboratorId, input.EventId);

        return Result<DefaultCollaboratorOutput>.Success(null, "Collaborator removed from event with success!");
    }
}