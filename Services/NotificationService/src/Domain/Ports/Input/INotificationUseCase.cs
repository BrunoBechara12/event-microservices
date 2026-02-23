using Domain.DTOs.Notification.Requests;
using Domain.DTOs.Notification.Responses;
using Result;

namespace Domain.Ports.Input;

public interface INotificationUseCase
{
    Task<Result<NotificationResponseDto>> SendNotification(SendNotificationRequestDto input);
    Task<Result<NotificationResponseDto>> SendTemplatedNotification(SendTemplatedNotificationRequestDto input);
    Task<Result<NotificationResponseDto>> GetById(int id);
    Task<Result<bool>> ResendFailedNotification(int notificationId);
    Task<Result<WhatsAppStatusResponseDto>> GetWhatsAppStatus();
}
