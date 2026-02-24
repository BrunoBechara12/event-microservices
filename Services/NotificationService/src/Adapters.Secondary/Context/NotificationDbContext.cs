using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adapters.Secondary.Context;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<MessageTemplate> MessageTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).ValueGeneratedOnAdd();
            entity.Property(n => n.PhoneNumber).IsRequired().HasMaxLength(20);
            entity.Property(n => n.Message).IsRequired().HasMaxLength(4096);
            entity.Property(n => n.Status).IsRequired();
            entity.Property(n => n.Type).IsRequired();
            entity.Property(n => n.ErrorMessage).HasMaxLength(1000);
            entity.Property(n => n.ExternalMessageId).HasMaxLength(100);

            entity.HasIndex(n => n.Status);
        });

        modelBuilder.Entity<MessageTemplate>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Template).IsRequired().HasMaxLength(4096);
            entity.Property(t => t.Type).IsRequired();

            entity.HasIndex(t => t.Type);
        });
    }
}
