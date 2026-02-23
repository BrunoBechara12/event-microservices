using Domain.Entities;

namespace Domain.Ports.Output;

public interface INotificationRepository
{
    Task<Notification?> GetById(int id);
    Task<IEnumerable<Notification>> GetPendingNotifications();
    Task<Notification> Create(Notification notification);
    Task<Notification> Update(Notification notification);
    Task Delete(int id);
}
