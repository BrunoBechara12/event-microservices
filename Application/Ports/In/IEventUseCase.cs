using Application.UseCases.Event.Inputs;
using Application.UseCases.Event.Outputs;
using Result;

namespace Application.Ports.In;
public interface IEventUseCase
{
    Task<Result<IEnumerable<DetailedEventOutput>>> Get();
    Task<Result<DetailedEventOutput>> GetById(int id);
    Task<Result<IEnumerable<DetailedEventOutput>>> GetByUserId(int userId);
    Task<Result<DefaultEventOutput>> Create(CreateEventInput input);
    Task<Result<DefaultEventOutput>> Update(UpdateEventInput updateEvent);
    Task<Result<DefaultEventOutput>> Delete(int id);
}
