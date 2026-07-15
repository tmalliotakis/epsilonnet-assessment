using EpsilonWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EpsilonWebApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.Property(e => e.CompanyName).HasMaxLength(200);
                entity.Property(e => e.ContactName).HasMaxLength(200);
                entity.Property(e => e.Address).HasMaxLength(500);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Region).HasMaxLength(100);
                entity.Property(e => e.PostalCode).HasMaxLength(20);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(50);
            });
        }
    }
}
