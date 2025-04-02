using FixManager.Api.Data;

namespace FixManager.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigurationDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}