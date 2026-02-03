using Domain.Ports.Input;
using Domain.Mappers;
using Domain.Entities;
using Domain.Ports.Output;
using Result;
using static Domain.Entities.Collaborator;
using Domain.DTOs.Collaborator.Requests;
using Domain.DTOs.Collaborator.Responses;

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

    public async Task<Result<DetailedCollaboratorResponseDto>> GetById(int id)
    {
        var collaborator = await _collaboratorRepository.GetById(id);

        var collaboratorOutput = collaborator?.ToDetailedResponseDto();

        if (collaborator == null)
        {
            return Result<DetailedCollaboratorResponseDto>.Success(collaboratorOutput, "Collaborator not found.");
        }

        return Result<DetailedCollaboratorResponseDto>.Success(collaboratorOutput, "Collaborator found with success!");
    }

    public async Task<Result<IEnumerable<DetailedCollaboratorResponseDto>>> GetByEventId(int eventId)
    {
        var eventExists = await _eventRepository.GetById(eventId);
        if (eventExists == null)
        {
            return Result<IEnumerable<DetailedCollaboratorResponseDto>>.Failure("Event not found.");
        }

        var collaborators = await _collaboratorRepository.GetByEventId(eventId);

        var collaboratorOutput = collaborators.Select(c => c.Collaborator.ToDetailedResponseDto()!);

        if (!collaborators.Any())
        {
            return Result<IEnumerable<DetailedCollaboratorResponseDto>>.Success(collaboratorOutput, "No collaborators found for this event.");
        }

        return Result<IEnumerable<DetailedCollaboratorResponseDto>>.Success(collaboratorOutput, "Collaborators found with success!");
    }

    public async Task<Result<DefaultCollaboratorResponseDto>> Create(CreateCollaboratorRequestDto input)
    {
        var newCollaborator = CreateCollaborator(input.UserId, input.Name);

        var createdCollaborator = await _collaboratorRepository.Create(newCollaborator);

        var collaboratorOutput = createdCollaborator.ToDefaultResponseDto();

        return Result<DefaultCollaboratorResponseDto>.Success(collaboratorOutput!, "Collaborator created with success!");
    }

    public async Task<Result<DefaultCollaboratorResponseDto>> Update(UpdateCollaboratorRequestDto input)
    {
        var collaboratorItem = await _collaboratorRepository.GetById(input.Id);

        if (collaboratorItem == null)
        {
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator not found.");
        }

        collaboratorItem.UpdateCollaborator(input.UserId, input.Name);

        await _collaboratorRepository.Update(collaboratorItem);

        var collaboratorOutput = collaboratorItem.ToDefaultResponseDto();

        return Result<DefaultCollaboratorResponseDto>.Success(collaboratorOutput!, "Collaborator updated with success!");
    }

    public async Task<Result<DefaultCollaboratorResponseDto>> Delete(int id)
    {
        var collaborator = await _collaboratorRepository.GetById(id);

        if (collaborator == null)
        {
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator not found.");
        }

        await _collaboratorRepository.Delete(collaborator);

        return Result<DefaultCollaboratorResponseDto>.Success(null, "Collaborator deleted with success!");
    }

    public async Task<Result<DefaultCollaboratorResponseDto>> AddToEvent(AddCollaboratorToEventRequestDto input)
    {
        var collaborator = await _collaboratorRepository.GetById(input.CollaboratorId);
        if (collaborator == null)
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator not found.");

        var eventItem = await _eventRepository.GetById(input.EventId);
        if (eventItem == null)
            return Result<DefaultCollaboratorResponseDto>.Failure("Event not found.");

        var isAlreadyLinked = await _collaboratorRepository.IsCollaboratorInEvent(input.CollaboratorId, input.EventId);
        if (isAlreadyLinked)
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator is already in this event.");

        var eventCollaborator = EventCollaborator.CreateEventCollaborator(input.EventId, input.CollaboratorId, input.Role);

        await _collaboratorRepository.AddToEvent(eventCollaborator);

        return Result<DefaultCollaboratorResponseDto>.Success(collaborator.ToDefaultResponseDto()!, "Collaborator added to event with success!");
    }

    public async Task<Result<DefaultCollaboratorResponseDto>> RemoveFromEvent(RemoveCollaboratorFromEventRequestDto input)
    {
        var collaborator = await _collaboratorRepository.GetById(input.CollaboratorId);
        if (collaborator == null)
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator not found.");

        var eventItem = await _eventRepository.GetById(input.EventId);
        if (eventItem == null)
            return Result<DefaultCollaboratorResponseDto>.Failure("Event not found.");

        var isLinked = await _collaboratorRepository.IsCollaboratorInEvent(input.CollaboratorId, input.EventId);
        if (!isLinked)
            return Result<DefaultCollaboratorResponseDto>.Failure("Collaborator is not linked to this event.");

        await _collaboratorRepository.RemoveFromEvent(input.CollaboratorId, input.EventId);

        return Result<DefaultCollaboratorResponseDto>.Success(null, "Collaborator removed from event with success!");
    }
}