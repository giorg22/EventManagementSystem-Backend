using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // ძირითადი ცხრილები
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

        modelBuilder.Entity<Ticket>()
    .Property(t => t.Price)
    .HasPrecision(18, 2);

        modelBuilder.Entity<Purchase>()
            .Property(p => p.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Participant>()
            .Property(p => p.PaidAmount)
            .HasPrecision(18, 2);

        //modelBuilder.Entity<Purchase>()
        //    .HasOne(p => p.Ticket)           // Purchase-ს აქვს ერთი Ticket
        //    .WithMany(t => t.Purchases)      // Ticket-ს აქვს ბევრი Purchase
        //    .HasForeignKey(p => p.TicketId)  // უთხარი, რომ გამოიყენოს ზუსტად ეს ველი!
        //    .OnDelete(DeleteBehavior.Restrict);

        //// 1. EVENT კონფიგურაცია
        //modelBuilder.Entity<Event>(entity =>
        //{
        //    entity.HasKey(e => e.Id);
        //    entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        //    entity.Property(e => e.Status)
        //        .HasConversion(v => v.ToString(), v => (EventStatus)Enum.Parse(typeof(EventStatus), v));
        //});

        //// 2. TICKET კონფიგურაცია
        //modelBuilder.Entity<Ticket>(entity =>
        //{
        //    entity.HasKey(t => t.Id);
        //    entity.Property(t => t.Price).HasPrecision(18, 2);

        //    entity.HasOne(t => t.Event)
        //        .WithMany(e => e.Tickets)
        //        .HasForeignKey(t => t.EventId)
        //        .OnDelete(DeleteBehavior.Cascade);
        //});

        //// 3. PURCHASE კონფიგურაცია
        //modelBuilder.Entity<Purchase>(entity =>
        //{
        //    entity.HasKey(p => p.Id);
        //    entity.Property(p => p.TotalAmount).HasPrecision(18, 2);

        //    entity.HasOne(p => p.Ticket)
        //        .WithMany()
        //        .HasForeignKey(p => p.TicketId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        //// 4. PARTICIPANT კონფიგურაცია
        //modelBuilder.Entity<Participant>(entity =>
        //{
        //    entity.HasKey(p => p.Id);
        //    entity.HasOne(p => p.Event)
        //        .WithMany()
        //        .HasForeignKey(p => p.EventId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        //// 5. RESOURCE კონფიგურაცია
        //modelBuilder.Entity<Resource>(entity =>
        //{
        //    entity.HasKey(r => r.Id);
        //    entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
        //});

        //// 6. RESERVATION კონფიგურაცია (რესურსების მართვა)
        //modelBuilder.Entity<Reservation>(entity =>
        //{
        //    entity.HasKey(r => r.Id);

        //    // კავშირი ღონისძიებასთან
        //    entity.HasOne(r => r.Event)
        //        .WithMany()
        //        .HasForeignKey(r => r.EventId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // კავშირი კონკრეტულ რესურსთან (დარბაზი/აღჭურვილობა)
        //    entity.HasOne(r => r.Resource)
        //        .WithMany()
        //        .HasForeignKey(r => r.ResourceId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //});

        //// 7. USER კონფიგურაცია
        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.HasKey(u => u.Id);
        //    entity.HasIndex(u => u.Email).IsUnique();
        //    entity.Property(u => u.Role)
        //        .HasConversion(v => v.ToString(), v => (UserRole)Enum.Parse(typeof(UserRole), v));
        //});
    }
}