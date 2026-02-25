using Domain.DTOs.Notification.Requests;
using Domain.Entities;
using Domain.Events;
using Domain.Ports.Input;
using Domain.Ports.Output;
using MassTransit;

namespace Adapters.Secondary.Messaging;

public class GuestConfirmedConsumer : IConsumer<GuestConfirmed>
{
    private readonly INotificationUseCase _notificationUseCase;
    private readonly IMessageTemplateRepository _templateRepository;

    public GuestConfirmedConsumer(INotificationUseCase notificationUseCase, IMessageTemplateRepository templateRepository)
    {
        _notificationUseCase = notificationUseCase;
        _templateRepository = templateRepository;
    }

    public async Task Consume(ConsumeContext<GuestConfirmed> context)
    {
        var message = context.Message;

        var template = await _templateRepository.GetByType(NotificationType.InviteConfirmation);
        
        string messageText;
        if (template != null)
        {
            messageText = template.FormatMessage(new Dictionary<string, string>
            {
                { "guestName", message.GuestName },
                { "eventName", message.EventName }
            });
        }
        else
        {
            messageText = $"Obrigado, {message.GuestName}! Sua presença no evento {message.EventName} foi confirmada.";
        }

        var input = new SendNotificationRequestDto(
            message.GuestPhoneNumber,
            messageText,
            NotificationType.InviteConfirmation
        );

        await _notificationUseCase.SendNotification(input);
    }
}
