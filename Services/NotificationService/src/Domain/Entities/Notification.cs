namespace Domain.Entities;

public class Notification
{
    public int Id { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Message { get; private set; }
    public NotificationStatus Status { get; private set; }
    public NotificationType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? ExternalMessageId { get; private set; }

    private Notification() { }

    public static Notification Create(string phoneNumber, string message, NotificationType type)
    {
        return new Notification
        {
            PhoneNumber = phoneNumber,
            Message = message,
            Type = type,
            Status = NotificationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void MarkAsSent(string? externalMessageId = null)
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTime.UtcNow;
        ExternalMessageId = externalMessageId;
    }

    public void MarkAsDelivered()
    {
        Status = NotificationStatus.Delivered;
    }

    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = NotificationStatus.Failed;
        ErrorMessage = errorMessage;
    }
}

public enum NotificationStatus
{
    Pending = 0,
    Sent = 1,
    Delivered = 2,
    Read = 3,
    Failed = 4
}

public enum NotificationType
{
    EventInvitation = 0,
    EventReminder = 1,
    EventUpdate = 2,
    EventCancellation = 3,
    InviteConfirmation = 4,
    Custom = 5
}
