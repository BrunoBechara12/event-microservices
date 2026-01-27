using Domain.Events;
using Domain.Ports.Output;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Adapters.Secondary.Messaging;

public class EventCreatedConsumer : IConsumer<EventCreated>
{
    private readonly IEventRepository _eventRepository;
    private readonly ILogger<EventCreatedConsumer> _logger;

    public EventCreatedConsumer(IEventRepository eventRepository, ILogger<EventCreatedConsumer> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EventCreated> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received EventCreated message: Id={Id}, Name={Name}", message.Id, message.Name);

        var existingEvent = await _eventRepository.GetById(message.Id);

        if (existingEvent is not null)
        {
            _logger.LogInformation("Event with Id={Id} already exists. Updating...", message.Id);
            existingEvent.UpdateEvent(message.Name);
            await _eventRepository.Update(existingEvent);
        }
        else
        {
            var newEvent = Domain.Entities.Event.CreateEvent(message.Id, message.Name);
            await _eventRepository.Create(newEvent);
            _logger.LogInformation("Event created successfully: Id={Id}, Name={Name}", message.Id, message.Name);
        }
    }
}
