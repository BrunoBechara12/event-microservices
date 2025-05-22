using Domain.Entities;
using Domain.Ports.Output;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using static Domain.Entities.Collaborator;

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
        var eventList = await _context.Events.ToListAsync();

        return eventList;
    }

    public async Task<Event> GetById(int id)
    {
        var eventList = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync();

        return eventList;
    }
    
    public async Task<IEnumerable<Event>> GetByUserId(int userId)
    {
        var eventList = await _context.Events.Where(x => x.OwnerUserId == userId).ToListAsync();

        return eventList;
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
        var eventDeleted = _context.Events.FirstOrDefault(a => a.Id == id);

        if (eventDeleted == null)
            return null;

        
        _context.Events.Remove(eventDeleted);
        await _context.SaveChangesAsync();

        return eventDeleted;
    }

    public Task<IEnumerable<Collaborator>> GetCollaborators(int eventId)
    {
        throw new NotImplementedException();
    }

    public Task AddCollaborator(int eventId, int userId, CollaboratorRole role)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCollaboratorRole(int eventId, int userId, CollaboratorRole role)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCollaborator(int eventId, int userId)
    {
        throw new NotImplementedException();
    }
}
