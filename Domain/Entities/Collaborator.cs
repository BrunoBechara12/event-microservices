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
        Validate(name);

        UserId = userId;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public static Collaborator Create(int userId, string name)
    {
        return new Collaborator(userId, name);
    }

    public void Update(int id, string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public static void Validate(string name)
    {
        if(name is null || name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long");
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
