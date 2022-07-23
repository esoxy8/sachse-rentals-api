using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace SachseRentalsApi.Entities
{
    public class SachseRentalsDb : DbContext
    {
        public SachseRentalsDb(DbContextOptions<SachseRentalsDb> options)
            : base(options)
        {}

        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Type)
                .HasConversion<int>();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.Status)
                .HasConversion<int>();

            base.OnModelCreating(modelBuilder);
        }
    }
}