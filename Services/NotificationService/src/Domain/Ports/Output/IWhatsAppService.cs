namespace Domain.Ports.Output;

public interface IWhatsAppService
{
    Task<WhatsAppSendResult> SendTextMessageAsync(string phoneNumber, string message);

    Task<WhatsAppSendResult> SendMediaMessageAsync(string phoneNumber, string mediaUrl, string? caption = null);
    Task<bool> IsConnectedAsync();
    Task<string?> GetQrCodeAsync();
}

public record WhatsAppSendResult(
    bool Success,
    string? MessageId = null,
    string? ErrorMessage = null
);
