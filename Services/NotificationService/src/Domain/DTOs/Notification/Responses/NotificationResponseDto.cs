namespace Domain.DTOs.Notification.Responses;

public record NotificationResponseDto(
    int Id,
    string PhoneNumber,
    string Message,
    string Status,
    string Type,
    DateTime CreatedAt,
    DateTime? SentAt,
    string? ErrorMessage
);
