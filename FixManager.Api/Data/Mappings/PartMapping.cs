using FixManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixManager.Api.Data.Mappings;

public class PartMapping : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.ToTable("Part");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("MONEY");
    }
}