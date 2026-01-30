using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public sealed class Guest
{
    public enum InviteStatus
    {
        Pending,
        Confirmed,
        Declined
    }

    [Key]
    public int Id { get; private set; }
    public int EventId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public InviteStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? ResponseDate { get; private set; }
    public Event Event { get; private set; } = null!;


    private Guest() { }

    private Guest(int eventId, string name, string email, string phoneNumber)
    {
        Validate(name, email, phoneNumber);

        EventId = eventId;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = InviteStatus.Pending;
        CreatedAt = DateTime.UtcNow; 
    }

    public static Guest CreateGuest(int eventId, string name, string email, string phoneNumber)
    {
        return new Guest(eventId, name, email, phoneNumber);
    }

    public void UpdateGuest(string name, string email, string phoneNumber, InviteStatus status)
    {
        Validate(name, email, phoneNumber);

        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AcceptInvite()
    {
        Status = InviteStatus.Confirmed;
        ResponseDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DeclineInvite()
    {
        Status = InviteStatus.Declined;
        ResponseDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate(string name, string email, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            throw new ArgumentException($"Name must be at least 3 characters long.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) 
            throw new ArgumentException("Invalid email format.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");
    }
}
