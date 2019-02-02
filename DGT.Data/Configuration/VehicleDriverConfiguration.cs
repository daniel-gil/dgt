using DGT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGT.Data.Configuration
{
    public class VehicleDriverConfiguration : IEntityTypeConfiguration<VehicleDriver>
    {
        public void Configure(EntityTypeBuilder<VehicleDriver> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("VehicleDrivers");
        }
    }
}
