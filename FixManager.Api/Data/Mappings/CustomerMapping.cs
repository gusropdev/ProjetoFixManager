using FixManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FixManager.Api.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("NVARCHAR");
        
        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(21)
            .HasColumnType("NVARCHAR");
        
        builder.HasMany(c => c.ServiceOrders)
            .WithOne(so => so.Customer) 
            .HasForeignKey(so => so.CustomerId) 
            .OnDelete(DeleteBehavior.Cascade); 
    }
}