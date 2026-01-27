using Adapters.Secondary.Context;
using Domain.Entities;
using Domain.Ports.Output;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Repositories;

public class EventRepository : IEventRepository
{
    private readonly GuestDbContext _context;

    public EventRepository(GuestDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>?> Get()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetById(int id)
    {
        return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> Create(Event eventEntity)
    {
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task Update(Event eventEntity)
    {
        _context.Events.Update(eventEntity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Event eventEntity)
    {
        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
    }
}
