using Domain.DTOs.Collaborator.Requests;
using Domain.DTOs.Collaborator.Responses;
using Result;

namespace Domain.Ports.Input;
public interface ICollaboratorUseCase
{
    Task<Result<DetailedCollaboratorResponseDto>> GetById(int id);
    Task<Result<IEnumerable<DetailedCollaboratorResponseDto>>> GetByEventId(int eventId);
    Task<Result<DefaultCollaboratorResponseDto>> Create(CreateCollaboratorRequestDto input);
    Task<Result<DefaultCollaboratorResponseDto>> Update(UpdateCollaboratorRequestDto input);
    Task<Result<DefaultCollaboratorResponseDto>> Delete(int id);
    Task<Result<DefaultCollaboratorResponseDto>> AddToEvent(AddCollaboratorToEventRequestDto input);
    Task<Result<DefaultCollaboratorResponseDto>> RemoveFromEvent(RemoveCollaboratorFromEventRequestDto input);
}
