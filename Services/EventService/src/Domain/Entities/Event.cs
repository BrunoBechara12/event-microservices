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
    public static Event CreateEvent(string name, string description, string location, DateTime startDate, int ownerUserId)
    {
        Validate(name, ownerUserId, startDate);

        return new Event(name, description, location, startDate, ownerUserId);
    }

    public void UpdateEvent(string name, string description, int ownerUserId, string location, DateTime startDate)
    {
        Validate(name, ownerUserId, startDate);

        Name = name;
        Description = description;
        Location = location;
        StartDate = startDate;
        UpdatedAt = DateTime.UtcNow; 
    }

    public static void Validate(string name, int ownerUserId, DateTime startDate)
    {
        if(name is null || name.Length < 3)
            throw new ArgumentException("Name must be at least 3 characters long");

        if (ownerUserId <= 0)
            throw new ArgumentException("Invalid owner");

        if (startDate < DateTime.Now)
            throw new ArgumentException("StartDate cannot be in the past");
    }

    public ICollection<EventCollaborator> EventCollaborators { get; set; }
}
