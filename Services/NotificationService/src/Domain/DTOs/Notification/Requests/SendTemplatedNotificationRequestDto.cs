namespace Domain.DTOs.Notification.Requests;

public record SendTemplatedNotificationRequestDto(
    string PhoneNumber,
    int TemplateId,
    Dictionary<string, string> Placeholders
);
