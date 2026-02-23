using Domain.DTOs.Notification.Requests;
using Domain.DTOs.Notification.Responses;
using Domain.Entities;
using Domain.Mappers;
using Domain.Ports.Input;
using Domain.Ports.Output;
using Result;

namespace Application.UseCases;

public class NotificationUseCase : INotificationUseCase
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMessageTemplateRepository _templateRepository;
    private readonly IWhatsAppService _whatsAppService;

    public NotificationUseCase(INotificationRepository notificationRepository, IMessageTemplateRepository templateRepository, IWhatsAppService whatsAppService)
    {
        _notificationRepository = notificationRepository;
        _templateRepository = templateRepository;
        _whatsAppService = whatsAppService;
    }

    public async Task<Result<NotificationResponseDto>> SendNotification(SendNotificationRequestDto input)
    {
        var notification = Notification.Create(
            input.PhoneNumber,
            input.Message,
            input.Type
        );

        await _notificationRepository.Create(notification);

        var result = await _whatsAppService.SendTextMessageAsync(input.PhoneNumber, input.Message);

        if (result.Success)
        {
            notification.MarkAsSent(result.MessageId);
        }
        else
        {
            notification.MarkAsFailed(result.ErrorMessage ?? "Unknown error");
        }

        await _notificationRepository.Update(notification);

        return Result<NotificationResponseDto>.Success(notification.ToResponseDto(), "Notification processed");
    }

    public async Task<Result<NotificationResponseDto>> SendTemplatedNotification(SendTemplatedNotificationRequestDto input)
    {
        var template = await _templateRepository.GetById(input.TemplateId);
        
        if (template == null)
        {
            return Result<NotificationResponseDto>.Failure($"Template not found with ID {input.TemplateId}");
        }

        if (!template.IsActive)
        {
            return Result<NotificationResponseDto>.Failure($"Template {input.TemplateId} is not active");
        }

        string formattedMessage = template.FormatMessage(input.Placeholders);

        var sendRequest = new SendNotificationRequestDto(
            input.PhoneNumber,
            formattedMessage,
            template.Type
        );

        return await SendNotification(sendRequest);
    }

    public async Task<Result<NotificationResponseDto>> GetById(int id)
    {
        var notification = await _notificationRepository.GetById(id);
        
        if (notification == null)
            return Result<NotificationResponseDto>.Failure($"Notification with Id={id} not found");

        return Result<NotificationResponseDto>.Success(notification.ToResponseDto(), "Notification found");
    }

    public async Task<Result<bool>> ResendFailedNotification(int notificationId)
    {
        var notification = await _notificationRepository.GetById(notificationId);
        
        if (notification == null)
            return Result<bool>.Failure($"Notification with Id={notificationId} not found");

        if (notification.Status != NotificationStatus.Failed)
            return Result<bool>.Failure("Only failed notifications can be resent");

        var result = await _whatsAppService.SendTextMessageAsync(notification.PhoneNumber, notification.Message);

        if (result.Success)
        {
            notification.MarkAsSent(result.MessageId);
            await _notificationRepository.Update(notification);
            return Result<bool>.Success(true, "Notification resent successfully");
        }

        notification.MarkAsFailed(result.ErrorMessage ?? "Unknown error");

        await _notificationRepository.Update(notification);

        return Result<bool>.Failure($"Failed to resend: {result.ErrorMessage}");
    }

    public async Task<Result<WhatsAppStatusResponseDto>> GetWhatsAppStatus()
    {
        var isConnected = await _whatsAppService.IsConnectedAsync();
        string? qrCode = null;

        if (!isConnected)
        {
            qrCode = await _whatsAppService.GetQrCodeAsync();
        }

        return Result<WhatsAppStatusResponseDto>.Success(new WhatsAppStatusResponseDto(isConnected, qrCode), "Status retrieved");
    }
}
