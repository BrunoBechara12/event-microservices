using Domain.Entities;
using Domain.Ports.Output;
using Adapters.Secondary.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Data.Repositories;

public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly EventDbContext _context;

    public CollaboratorRepository(EventDbContext context)
    {
        _context = context;
    }

    public async Task<Collaborator?> GetById(int id)
    {
        return await _context.Collaborators.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<EventCollaborator>> GetByEventId(int eventId)
    {
        return await _context.EventCollaborators
            .Include(ec => ec.Collaborator)
            .Where(ec => ec.EventId == eventId)
            .ToListAsync();
    }

    public async Task<Collaborator> Create(Collaborator collaborator)
    {
        var newCollaborator = await _context.Collaborators.AddAsync(collaborator);

        await _context.SaveChangesAsync();

        return newCollaborator.Entity;
    }

    public async Task Update(Collaborator collaborator)
    {
        _context.Collaborators.Update(collaborator);

        await _context.SaveChangesAsync();
    }
    public async Task Delete(Collaborator collaborator)
    {
        _context.Collaborators.Remove(collaborator);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsCollaboratorInEvent(int collaboratorId, int eventId)
    {
        return await _context.EventCollaborators
            .AnyAsync(ec => ec.CollaboratorId == collaboratorId && ec.EventId == eventId);
    }

    public async Task AddToEvent(EventCollaborator eventCollaborator)
    {
        await _context.EventCollaborators.AddAsync(eventCollaborator);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromEvent(int collaboratorId, int eventId)
    {
        var eventCollaborator = await _context.EventCollaborators
            .FirstOrDefaultAsync(ec => ec.CollaboratorId == collaboratorId && ec.EventId == eventId);

        if (eventCollaborator != null)
        {
            _context.EventCollaborators.Remove(eventCollaborator);
            await _context.SaveChangesAsync();
        }
    }
}