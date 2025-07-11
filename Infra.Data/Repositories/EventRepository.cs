using Domain.Entities;
using Domain.Ports.Output;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;
public class EventRepository : IEventRepository
{
    private readonly EventDbContext _context;

    public EventRepository(EventDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        var events = await _context.Events.ToListAsync();

        return events;
    }

    public async Task<Event?> GetById(int id)
    {
        var events = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();

        return events;
    }
    
    public async Task<IEnumerable<Event>> GetByUserId(int userId)
    {
        var events = await _context.Events.Where(x => x.OwnerUserId == userId).ToListAsync();

        return events;
    }

    public async Task<Event> Create(Event createEvent)
    {
        var eventItem = _context.Events.AddAsync(createEvent);
        await _context.SaveChangesAsync();

        return eventItem.Result.Entity;
    }

    public async Task<Event> Update(Event updateEvent)
    {

        var eventItem = _context.Events.FirstOrDefault(a => a.Id == updateEvent.Id);

        if (eventItem == null)
            return null;

        eventItem.Name = updateEvent.Name;
        eventItem.Description = updateEvent.Description;
        eventItem.Location = updateEvent.Location;
        eventItem.StartDate = updateEvent.StartDate;
        eventItem.OwnerUserId = updateEvent.OwnerUserId;
        eventItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return eventItem;
    }

    public async Task<Event> Delete(int id)
    {
        var deletedEvent = _context.Events.FirstOrDefault(a => a.Id == id);

        if (deletedEvent == null)
            return null;

        _context.Events.Remove(deletedEvent);
        await _context.SaveChangesAsync();

        return deletedEvent;
    }
}
