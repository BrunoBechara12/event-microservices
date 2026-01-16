using Application.UseCases.Inputs;
using Application.UseCases.Outputs;
using Result;

namespace Application.Ports.Input;
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
