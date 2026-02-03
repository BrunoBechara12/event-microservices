using Domain.DTOs.Guest.Requests;
using Domain.DTOs.Guest.Responses;
using Result;

namespace Domain.Ports.Input;
public interface IGuestUseCase
{
    Task<Result<IEnumerable<DetailedGuestResponseDto>>> Get();
    Task<Result<DetailedGuestResponseDto>> GetById(int id);
    Task<Result<DefaultGuestResponseDto>> Create(CreateGuestRequestDto input);
    Task<Result<DefaultGuestResponseDto>> Update(UpdateGuestRequestDto input);
    Task<Result<DefaultGuestResponseDto>> Delete(int id);
    Task<Result<DefaultGuestResponseDto>> AcceptInvite(InviteResponseRequestDto input);
    Task<Result<DefaultGuestResponseDto>> DeclineInvite(InviteResponseRequestDto input);
}
