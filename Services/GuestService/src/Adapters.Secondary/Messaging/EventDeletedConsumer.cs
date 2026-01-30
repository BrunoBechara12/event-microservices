using Domain.Events;
using Domain.Ports.Output;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Adapters.Secondary.Messaging;
public class EventDeletedConsumer : IConsumer<EventDeleted>
{
    private readonly IEventRepository _eventRepository;
    private readonly ILogger<EventDeletedConsumer> _logger;

    public EventDeletedConsumer(IEventRepository eventRepository, ILogger<EventDeletedConsumer> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EventDeleted> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received EventDeleted message: Id={Id}", message.Id);

        var existingEvent = await _eventRepository.GetById(message.Id);

        if (existingEvent is not null)
        {
            await _eventRepository.Delete(existingEvent);
            _logger.LogInformation("Event deleted successfully: Id={Id}", message.Id);
        }
        else
        {
            _logger.LogWarning("Event with Id={Id} not found for deletion.", message.Id);
        }
    }
}
