using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Collaborator
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Collaborator() { }

    public Collaborator(int id, int userId)
    {
        Id = id;
        UserId = userId;
        UpdatedAt = DateTime.Now;
    }

    public Collaborator(int userId, string name)
    {
        UserId = userId;
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
