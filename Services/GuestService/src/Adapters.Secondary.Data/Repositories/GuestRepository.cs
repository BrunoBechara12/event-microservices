using Adapters.Secondary.Data.Context;
using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Data.Repositories;
public class GuestRepository : IGuestRepository
{
    private readonly GuestDbContext _context;

    public GuestRepository(GuestDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guest>?> Get()
    {
        return await _context.Guests.ToListAsync();
    }

    public async Task<Guest?> GetById(int id)
    {
        return await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Guest> Create(Guest guest)
    {
        var newGuest = await _context.Guests.AddAsync(guest);

        await _context.SaveChangesAsync();

        return newGuest.Entity;
    }

    public async Task Update(Guest guest)
    {
        _context.Guests.Update(guest);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guest guest)
    {
        _context.Guests.Remove(guest);

        await _context.SaveChangesAsync();
    }
}
