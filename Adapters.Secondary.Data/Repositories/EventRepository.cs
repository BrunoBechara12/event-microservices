using Domain.Entities;
using Domain.Ports.Output;
using Adapters.Secondary.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Data.Repositories;
public class EventRepository : IEventRepository
{
    private readonly EventDbContext _context;

    public EventRepository(EventDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> Get()
    {
        var events = await _context.Events.ToListAsync();

        return events;
    }

    public async Task<Event?> GetById(int id)
    {
        var eventItem = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();

        return eventItem;
    }
    
    public async Task<IEnumerable<Event>> GetByUserId(int userId)
    {
        var events = await _context.Events.Where(x => x.OwnerUserId == userId).ToListAsync();

        return events;
    }

    public async Task<Event> Create(Event createEvent)
    {
        var eventCreated = await _context.Events.AddAsync(createEvent);

        await _context.SaveChangesAsync();

        return eventCreated.Entity;
    }

    public async Task Update(Event updateEvent)
    {
        _context.Events.Update(updateEvent);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Event eventItem)
    {
        _context.Events.Remove(eventItem);

        await _context.SaveChangesAsync();
    }
}
