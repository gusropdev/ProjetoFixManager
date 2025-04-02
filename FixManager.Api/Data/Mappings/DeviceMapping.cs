using FixManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixManager.Api.Data.Mappings;

public class DeviceMapping : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Device");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.Brand)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.Model)
            .HasMaxLength(60)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.SerialNumber)
            .HasMaxLength(60)
            .HasColumnType("VARCHAR");
        
    }
}