using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Data.Context;
public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<EventCollaborator> EventCollaborators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventCollaborator>()
            .HasOne(ec => ec.Event)
            .WithMany(e => e.EventCollaborators)
            .HasForeignKey(ec => ec.EventId);

        modelBuilder.Entity<EventCollaborator>()
            .HasOne(ec => ec.Collaborator)
            .WithMany(c => c.EventCollaborators)
            .HasForeignKey(ec => ec.CollaboratorId);

        modelBuilder.Entity<EventCollaborator>()
            .HasIndex(ec => new { ec.EventId, ec.CollaboratorId })
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}

