using Domain.DTOs.Event.Requests;
using Domain.DTOs.Event.Responses;
using Result;

namespace Domain.Ports.Input;
public interface IEventUseCase
{
    Task<Result<IEnumerable<DetailedEventResponseDto>>> Get();
    Task<Result<DetailedEventResponseDto>> GetById(int id);
    Task<Result<IEnumerable<DetailedEventResponseDto>>> GetByUserId(int userId);
    Task<Result<DefaultEventResponseDto>> Create(CreateEventRequestDto input);
    Task<Result<DefaultEventResponseDto>> Update(UpdateEventRequestDto updateEvent);
    Task<Result<DefaultEventResponseDto>> Delete(int id);
}
