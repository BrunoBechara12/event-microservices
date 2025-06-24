using Domain.Entities;
using Domain.Ports.Output;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories;
public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly EventDbContext _context;

    public CollaboratorRepository(EventDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Collaborator>> GetByEventId(int eventId)
    {
        var collaborators = await _context.Collaborators.Where(c => c.EventCollaborators.Any(ec => ec.EventId == eventId)).ToListAsync();

        return collaborators;
    }

    public async Task<Collaborator> Create(Collaborator collaborator, EventCollaborator eventCollaborator)
    {
        var eventItem = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventCollaborator.EventId);
        if (eventItem == null)
            throw new Exception("Event not found");

        var existingCollaborator = _context.Collaborators
            .FirstOrDefault(c => c.UserId == collaborator.UserId);

        Collaborator returnCollaborator;

        if (existingCollaborator == null)
        {
            var addedCollaborator = await _context.Collaborators.AddAsync(collaborator);
            await _context.SaveChangesAsync();

            returnCollaborator = addedCollaborator.Entity;
        }
        else
        {
            returnCollaborator = existingCollaborator;
        }

        eventCollaborator.CollaboratorId = returnCollaborator.Id;

        await _context.EventCollaborators.AddAsync(eventCollaborator);
        await _context.SaveChangesAsync();

        returnCollaborator.EventCollaborators = new List<EventCollaborator> { eventCollaborator };

        return returnCollaborator;
    }

    public async Task<EventCollaborator> UpdateRole(EventCollaborator eventCollaborator)
    {
        var collaboratorItem = await _context.EventCollaborators.FirstOrDefaultAsync(c => c.Id == eventCollaborator.Id);

        if (collaboratorItem == null)
            return null;

        collaboratorItem.Role = eventCollaborator.Role;
        collaboratorItem.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return collaboratorItem;
    }

    public async Task<EventCollaborator> Remove(int collaboratorId, int eventId)
    {

        var eventCollaborator = await _context.EventCollaborators.Where(a => a.EventId == eventId && a.CollaboratorId == collaboratorId).FirstOrDefaultAsync();

        if (eventCollaborator == null)
            return null;

        _context.EventCollaborators.Remove(eventCollaborator);
        await _context.SaveChangesAsync();

        return eventCollaborator;
    }

}
