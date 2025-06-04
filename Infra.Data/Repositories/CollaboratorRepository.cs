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

    public async Task<Collaborator> Create(Collaborator collaborator, EventCollaborator eventCollaborator)
    {
        var eventItem = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventCollaborator.EventId);
        if (eventItem == null)
            throw new Exception("Event not found");

        var existingCollaborator = _context.Collaborators
            .FirstOrDefault(c => c.UserId == collaborator.UserId);

        Collaborator collaboratorToReturn;

        if (existingCollaborator == null)
        {
            var addedCollaborator = await _context.Collaborators.AddAsync(collaborator);
            await _context.SaveChangesAsync(); 

            collaboratorToReturn = addedCollaborator.Entity;
        }
        else
        {
            collaboratorToReturn = existingCollaborator;
        }

        eventCollaborator.CollaboratorId = collaboratorToReturn.Id;

        await _context.EventCollaborators.AddAsync(eventCollaborator);
        await _context.SaveChangesAsync();

        collaboratorToReturn.EventCollaborators = new List<EventCollaborator> { eventCollaborator };

        return collaboratorToReturn;
    }

    public Task<IEnumerable<Collaborator>> Get(int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<Collaborator> Remove(int collaboratorId, int eventId)
    {
        throw new NotImplementedException();
    }

    public Task<Collaborator> UpdateRole(Collaborator collaborator)
    {
        throw new NotImplementedException();
    }
    
}
