using Domain.DTOs.Notification.Requests;
using Domain.Entities;
using Domain.Events;
using Domain.Ports.Input;
using Domain.Ports.Output;
using MassTransit;

namespace Adapters.Secondary.Messaging;
public class GuestInvitedConsumer : IConsumer<GuestInvited>
{
    private readonly INotificationUseCase _notificationUseCase;
    private readonly IMessageTemplateRepository _templateRepository;

    public GuestInvitedConsumer(INotificationUseCase notificationUseCase, IMessageTemplateRepository templateRepository)
    {
        _notificationUseCase = notificationUseCase;
        _templateRepository = templateRepository;
    }

    public async Task Consume(ConsumeContext<GuestInvited> context)
    {
        var message = context.Message;

        var template = await _templateRepository.GetByType(NotificationType.EventInvitation);
        
        string messageText;
        if (template != null)
        {
            messageText = template.FormatMessage(new Dictionary<string, string>
            {
                { "guestName", message.GuestName },
                { "eventName", message.EventName },
                { "eventDate", message.EventDate.ToString("dd/MM/yyyy HH:mm") }
            });
        }
        else
        {
            messageText = $"Olá {message.GuestName}! Você foi convidado para o evento {message.EventName} em {message.EventDate:dd/MM/yyyy HH:mm}.";
        }

        var input = new SendNotificationRequestDto(
            message.GuestPhoneNumber,
            messageText,
            NotificationType.EventInvitation
        );

        await _notificationUseCase.SendNotification(input);
    }
}
