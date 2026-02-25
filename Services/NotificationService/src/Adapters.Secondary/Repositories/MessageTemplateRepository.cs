using Adapters.Secondary.Context;
using Domain.Entities;
using Domain.Ports.Output;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Repositories;

public class MessageTemplateRepository : IMessageTemplateRepository
{
    private readonly NotificationDbContext _context;

    public MessageTemplateRepository(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task<MessageTemplate?> GetById(int id)
    {
        return await _context.MessageTemplates.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<MessageTemplate?> GetByType(NotificationType type)
    {
        return await _context.MessageTemplates
            .Where(t => t.Type == type && t.IsActive)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MessageTemplate>> GetAll()
    {
        return await _context.MessageTemplates
            .OrderBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<MessageTemplate> Create(MessageTemplate template)
    {
        await _context.MessageTemplates.AddAsync(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<MessageTemplate> Update(MessageTemplate template)
    {
        _context.MessageTemplates.Update(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task Delete(int id)
    {
        var template = await _context.MessageTemplates.FindAsync(id);
        if (template != null)
        {
            _context.MessageTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}
