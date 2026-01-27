using Domain.Ports.Output;
using MassTransit;
namespace Adapters.Secondary.MessageHandler;
public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(message!, cancellationToken);
    }
}
