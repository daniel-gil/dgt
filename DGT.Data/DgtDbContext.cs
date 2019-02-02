using DGT.Models;
using Microsoft.EntityFrameworkCore;

namespace DGT.Data
{
    public class DgtDbContext : DbContext
    {
        public DgtDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Infraction> Infractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
