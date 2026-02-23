namespace Domain.DTOs.Notification.Responses;

public record WhatsAppStatusResponseDto(
    bool IsConnected,
    string? QrCode
);
