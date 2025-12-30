using System.ComponentModel.DataAnnotations;

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
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public InviteStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? ResponseDate { get; private set; }

    private Guest() { }

    private Guest(string name, string email, string phoneNumber)
    {
        Validate(name, email, phoneNumber);

        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = InviteStatus.Pending;
        CreatedAt = DateTime.UtcNow; 
    }

    public static Guest CreateGuest(string name, string email, string phoneNumber)
    {
        return new Guest(name, email, phoneNumber);
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
