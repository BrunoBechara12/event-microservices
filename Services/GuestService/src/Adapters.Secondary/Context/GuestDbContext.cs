using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Context;
public class GuestDbContext : DbContext
{
    public GuestDbContext(DbContextOptions<GuestDbContext> options) : base(options)
    { }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone");

            entity.Property(e => e.ResponseDate)
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone");
        });
    }
}
