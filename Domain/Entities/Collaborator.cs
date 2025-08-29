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
    private Collaborator(string name)
    {
        Validate(name);
        Name = name;
    }

    public static Collaborator Create(int userId, string name)
    {
        return new Collaborator(name)
        {
            UserId = userId,
            CreatedAt = DateTime.Now
        };
    }

    public static Collaborator Update(int id, string name)
    {
        return new Collaborator(name)
        {
            Id = id,
            UpdatedAt = DateTime.Now
        };
    }
    
    public static void Validate(string name)
    {
        if(name is null || name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long");
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
