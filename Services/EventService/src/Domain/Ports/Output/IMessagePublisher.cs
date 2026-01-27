namespace Domain.Ports.Output;
public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
}
