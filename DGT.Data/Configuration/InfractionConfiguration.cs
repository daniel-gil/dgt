using DGT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DGT.Data.Configuration
{
    public class InfractionConfiguration : IEntityTypeConfiguration<Infraction>
    {
        public void Configure(EntityTypeBuilder<Infraction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.ToTable("Infractions");
        }
    }
}
