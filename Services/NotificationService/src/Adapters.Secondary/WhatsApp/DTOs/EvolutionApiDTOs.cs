namespace Adapters.Secondary.WhatsApp.DTOs;

public class SendTextRequest
{
    public string Number { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public class SendMediaRequest
{
    public string Number { get; set; } = string.Empty;
    public string Media { get; set; } = string.Empty;
    public string? Caption { get; set; }
}

public class SendMessageResponse
{
    public MessageKey? Key { get; set; }
}

public class MessageKey
{
    public string? Id { get; set; }
}

public class ConnectionStateResponse
{
    public InstanceState? Instance { get; set; }
}

public class InstanceState
{
    public string? State { get; set; }
}

public class QrCodeResponse
{
    public string? Base64 { get; set; }
    public string? Code { get; set; }
}
