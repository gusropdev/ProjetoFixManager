using FixManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixManager.Api.Data.Mappings;

public class ServiceOrderMapping : IEntityTypeConfiguration<ServiceOrder>
{
    public void Configure(EntityTypeBuilder<ServiceOrder> builder)
    {
        builder.ToTable("ServiceOrder");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(x => x.ReportedIssue)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.Diagnosis)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.EstimatedCost)
            .HasColumnType("MONEY");
        
        builder.HasOne(so => so.Device)
            .WithOne(d => d.ServiceOrder)
            .HasForeignKey<Device>(d =>  d.ServiceOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(so => so.Parts)
            .WithOne(p => p.ServiceOrder) 
            .HasForeignKey(p => p.ServiceOrderId) 
            .OnDelete(DeleteBehavior.Cascade); 
        
    }
}