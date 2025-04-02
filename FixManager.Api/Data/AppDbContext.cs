using System.Reflection;
using FixManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Device> Devices { get; set; }  = null!;
    public DbSet<ServiceOrder> ServiceOrders { get; set; }  = null!;
    public DbSet<Part> Parts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}