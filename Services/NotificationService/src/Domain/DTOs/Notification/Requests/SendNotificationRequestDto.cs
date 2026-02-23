using Domain.Entities;

namespace Domain.DTOs.Notification.Requests;

public record SendNotificationRequestDto(
    string PhoneNumber,
    string Message,
    NotificationType Type
);
