using FixManager.Api.Data;
using FixManager.Api.Handlers;
using FixManager.Core;
using FixManager.Core.Handlers;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(t => t.FullName));
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(
            x =>
            {
                x.UseSqlServer(Configuration.ConnectionString);
            });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICustomerHandler, CustomerHandler>();
        builder.Services.AddTransient<IServiceOrderHandler, ServiceOrderHandler>();
        builder.Services.AddTransient<IDeviceHandler, DeviceHandler>();
        builder.Services.AddTransient<IPartHandler, PartHandler>();
    }
}