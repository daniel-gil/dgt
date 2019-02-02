using DGT.Models;
using Microsoft.EntityFrameworkCore;
using DGT.Data.Configuration;

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
        public DbSet<VehicleInfraction> VehicleInfractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DriverConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new InfractionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
