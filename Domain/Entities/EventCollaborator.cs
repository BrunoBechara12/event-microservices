
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class EventCollaborator
{
    public enum CollaboratorRole
    {
        Adm,
        Inviter
    }

    [Key]
    public int Id { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; }

    public int CollaboratorId { get; set; }
    public Collaborator Collaborator { get; set; }

    public CollaboratorRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public EventCollaborator() { }

    public EventCollaborator(int eventId, int collaboratorId, CollaboratorRole role)
    {
        EventId = eventId;
        CollaboratorId = collaboratorId;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public EventCollaborator(int id, CollaboratorRole role)
    {
        Id = id;
        Role = role;
        UpdatedAt = DateTime.UtcNow;
    }
}
