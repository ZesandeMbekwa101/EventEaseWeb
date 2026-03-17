using Microsoft.EntityFrameworkCore;
using EventEaseWeb.Models;

namespace EventEaseWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<UserModel> Users { get; set; }
        public DbSet<VenueModel> Venues { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User table configuration
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserID);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasDefaultValue("Booking Specialist");

                entity.HasIndex(e => e.Email).IsUnique();
            });


            // Venue configuration
            modelBuilder.Entity<VenueModel>(entity =>
            {
                entity.ToTable("Venues");
                entity.HasKey(e => e.VenueID);

                entity.Property(e => e.VenueName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.Image).HasMaxLength(256);
            });

            // Event configuration
            modelBuilder.Entity<EventModel>(entity =>
            {
                entity.ToTable("Events");
                entity.HasKey(e => e.EventID);

                entity.Property(e => e.EventName).IsRequired().HasMaxLength(150);
                entity.Property(e => e.EventDate).IsRequired().HasColumnType("date");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.VenueID).IsRequired();

                entity.HasOne(e => e.Venue)
                      .WithMany()
                      .HasForeignKey(e => e.VenueID)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<BookingModel>(entity =>
            {
                entity.ToTable("Bookings");

                entity.HasKey(b => b.BookingID);

                entity.Property(b => b.BookingDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasOne(b => b.Event)
                    .WithMany()
                    .HasForeignKey(b => b.EventID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Venue)
                    .WithMany()
                    .HasForeignKey(b => b.VenueID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.User)
                    .WithMany()
                    .HasForeignKey(b => b.BookedBy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}