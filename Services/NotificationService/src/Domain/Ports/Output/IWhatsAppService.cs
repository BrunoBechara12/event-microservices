namespace Domain.Ports.Output;

/// <summary>
/// Port for WhatsApp messaging service integration (EvolutionAPI)
/// </summary>
public interface IWhatsAppService
{
    /// <summary>
    /// Sends a text message to a phone number
    /// </summary>
    /// <param name="phoneNumber">Phone number with country code (e.g., 5511999999999)</param>
    /// <param name="message">Text message to send</param>
    /// <returns>External message ID if successful</returns>
    Task<WhatsAppSendResult> SendTextMessageAsync(string phoneNumber, string message);

    /// <summary>
    /// Sends a media message (image, document, etc.)
    /// </summary>
    Task<WhatsAppSendResult> SendMediaMessageAsync(string phoneNumber, string mediaUrl, string? caption = null);

    /// <summary>
    /// Checks if the WhatsApp instance is connected
    /// </summary>
    Task<bool> IsConnectedAsync();

    /// <summary>
    /// Gets the QR code for connecting the WhatsApp instance
    /// </summary>
    Task<string?> GetQrCodeAsync();
}

public record WhatsAppSendResult(
    bool Success,
    string? MessageId = null,
    string? ErrorMessage = null
);
