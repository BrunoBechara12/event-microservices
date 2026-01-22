using Domain.Contracts.Guest.Inputs;
using Domain.Contracts.Guest.Outputs;
using Result;

namespace Domain.Ports.In;
public interface IGuestUseCase
{
    Task<Result<IEnumerable<DetailedGuestOutput>>> Get();
    Task<Result<DetailedGuestOutput>> GetById(int id);
    Task<Result<DefaultGuestOutput>> Create(CreateGuestInput input);
    Task<Result<DefaultGuestOutput>> Update(UpdateGuestInput input);
    Task<Result<DefaultGuestOutput>> Delete(int id);
    Task<Result<DefaultGuestOutput>> AcceptInvite(InviteResponseInput input);
    Task<Result<DefaultGuestOutput>> DeclineInvite(InviteResponseInput input);
}
