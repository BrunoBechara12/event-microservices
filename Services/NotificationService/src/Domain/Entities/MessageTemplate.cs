namespace Domain.Entities;

public class MessageTemplate
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public NotificationType Type { get; private set; }
    public string Template { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private MessageTemplate() { }

    public static MessageTemplate Create(string name, NotificationType type, string template)
    {
        return new MessageTemplate
        {
            Name = name,
            Type = type,
            Template = template,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Update(string name, string template)
    {
        Name = name;
        Template = template;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public string FormatMessage(Dictionary<string, string> placeholders)
    {
        var message = Template;
        foreach (var placeholder in placeholders)
        {
            message = message.Replace($"{{{placeholder.Key}}}", placeholder.Value);
        }
        return message;
    }
}
