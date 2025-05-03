using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Collaborator
{
    public enum CollaboratorRole
    {
        Adm,
        Inviter
    }

    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public CollaboratorRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Collaborator() { }

    public ICollection<Event> Events { get; set; }
}
