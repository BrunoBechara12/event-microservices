using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Event
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public int OwnerUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Event() { }
    
    private Event(string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        Validate(name, startDate);
        Name = name;
        Description = description;
        Location = location;
        StartDate = startDate;
        OwnerUserId = ownerUserId;
    }

    public static Event Create(string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        return new Event(name, description, location, startDate, ownerUserId)
        {
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static Event Update(int id, string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        return new Event(name, description, location, startDate, ownerUserId)
        {
            Id = id,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static void Validate(string name, DateTime startDate)
    {
        if(name is null || name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long");

        if(startDate < DateTime.Now)
            throw new ArgumentException("StartDate cannot be in the past");
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
