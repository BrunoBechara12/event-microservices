using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Collaborator
{
    [Key]
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Collaborator() { }
    private Collaborator(int userId, string name)
    {
        Validate(userId, name);

        UserId = userId;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public static Collaborator CreateCollaborator(int userId, string name)
    {
        Validate(userId, name);

        return new Collaborator(userId, name);
    }

    public void UpdateCollaborator(int userId, string name)
    {
        Validate(userId, name);

        UserId = userId;
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public static void Validate(int userId, string name)
    {
        if (userId <= 0)
            throw new ArgumentException("Invalid user id");

        if (name is null || name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long");
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
