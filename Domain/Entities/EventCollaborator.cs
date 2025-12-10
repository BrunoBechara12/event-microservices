using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;
public sealed class EventCollaborator
{
    public enum CollaboratorRole
    {
        Adm,
        Inviter
    }

    [Key]
    public int Id { get; private set; }
    public int EventId { get; private set; }
    public int CollaboratorId { get; private set; }
    public CollaboratorRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    [JsonIgnore]
    public Event Event { get; private set; }
    [JsonIgnore]
    public Collaborator Collaborator { get; private set; }

    private EventCollaborator() { }

    public EventCollaborator(int eventId, int collaboratorId, CollaboratorRole role)
    {
        if(eventId <= 0) throw new ArgumentException("Invalid EventId");
        if(collaboratorId <= 0) throw new ArgumentException("Invalid CollaboratorId");

        EventId = eventId;
        CollaboratorId = collaboratorId;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateRole(CollaboratorRole newRole)
    {
        if (Role == newRole) return;

        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }
}
