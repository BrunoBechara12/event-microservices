using Domain.Contracts.Event.Inputs;
using Domain.Contracts.Event.Outputs;
using Result;

namespace Domain.Ports.In;
public interface IEventUseCase
{
    Task<Result<IEnumerable<DetailedEventOutput>>> Get();
    Task<Result<DetailedEventOutput>> GetById(int id);
    Task<Result<IEnumerable<DetailedEventOutput>>> GetByUserId(int userId);
    Task<Result<DefaultEventOutput>> Create(CreateEventInput input);
    Task<Result<DefaultEventOutput>> Update(UpdateEventInput updateEvent);
    Task<Result<DefaultEventOutput>> Delete(int id);
}
