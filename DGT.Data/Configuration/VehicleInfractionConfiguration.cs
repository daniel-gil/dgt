using DGT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGT.Data.Configuration
{
    public class VehicleInfractionConfiguration : IEntityTypeConfiguration<VehicleInfraction>
    {
        public void Configure(EntityTypeBuilder<VehicleInfraction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InfractionDate).IsRequired();
            builder.ToTable("VehicleInfractions");
        }
    }
}
