using DGT.Models;
using Microsoft.EntityFrameworkCore;

namespace DGT.Data
{
    public class DgtDbContext : DbContext
    {
        public DgtDbContext(DbContextOptions<DgtDbContext> options)
           : base(options)
        {
            this.Database.EnsureCreated();

        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
