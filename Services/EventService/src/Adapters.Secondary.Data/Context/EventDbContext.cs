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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Collaborator>(entity =>
        {
            entity.Property(c => c.CreatedAt)
                .HasColumnType("timestamp without time zone");

            entity.Property(c => c.UpdatedAt)
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<EventCollaborator>(entity =>
        {
            entity.HasOne(ec => ec.Event)
                .WithMany(e => e.EventCollaborators)
                .HasForeignKey(ec => ec.EventId);

            entity.HasOne(ec => ec.Collaborator)
                .WithMany(c => c.EventCollaborators)
                .HasForeignKey(ec => ec.CollaboratorId);

            entity.HasIndex(ec => new { ec.EventId, ec.CollaboratorId })
                .IsUnique();

            entity.Property(ec => ec.CreatedAt)
                .HasColumnType("timestamp without time zone");
        });
    }
}