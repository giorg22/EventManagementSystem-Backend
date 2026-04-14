using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Artist> Artists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasOne(e => e.Hall)
                .WithMany()
                .HasForeignKey(e => e.HallId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Artists)
                .WithMany(a => a.Events);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasOne(p => p.Ticket)
                .WithMany(t => t.Purchases)
                .HasForeignKey(p => p.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Status)
                .HasConversion<string>();
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasOne(p => p.Ticket)
                .WithMany()
                .HasForeignKey(p => p.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Event)
                .WithMany()
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.User)
                .WithMany(u => u.Participations)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.HasMany(h => h.Resources)
                .WithOne(r => r.Hall)
                .HasForeignKey(r => r.HallId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(r => r.Event)
                .WithMany()
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}