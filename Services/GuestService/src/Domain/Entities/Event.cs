using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public sealed class Event
{
    [Key]
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Event() { }

    private Event(int id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public static Event CreateEvent(int id, string name)
    {
        return new Event(id, name);
    }

    public void UpdateEvent(string name)
    {
        Name = name;
    }
}
