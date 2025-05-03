using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;
public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>()
        .HasMany(x => x.Collaborators)
        .WithMany(x => x.Events)
        .UsingEntity(j => j.ToTable("EventCollaborators"));

        base.OnModelCreating(modelBuilder);
    }
}

