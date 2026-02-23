using Domain.Entities;

namespace Domain.Ports.Output;

public interface IMessageTemplateRepository
{
    Task<MessageTemplate?> GetById(int id);
    Task<MessageTemplate?> GetByType(NotificationType type);
    Task<IEnumerable<MessageTemplate>> GetAll();
    Task<MessageTemplate> Create(MessageTemplate template);
    Task<MessageTemplate> Update(MessageTemplate template);
    Task Delete(int id);
}
