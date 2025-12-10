using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public sealed class Event
{
    [Key]
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public DateTime StartDate { get; private set; }
    public int OwnerUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Event() { }
    private Event(string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        Name = name;
        Description = description;
        Location = location;
        StartDate = startDate;
        OwnerUserId = ownerUserId;
        CreatedAt = DateTime.UtcNow;
    }
    public static Event Create(string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        Validate(name, startDate);

        return new Event(name, description, location, startDate, ownerUserId);
    }

    public void Update(string name, string description, string location, DateTime startDate)
    {
        Validate(name, startDate);

        Name = name;
        Description = description;
        Location = location;
        StartDate = startDate;
        UpdatedAt = DateTime.UtcNow; 
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
