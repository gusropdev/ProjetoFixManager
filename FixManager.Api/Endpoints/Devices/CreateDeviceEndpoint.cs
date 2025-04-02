using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Devices;

public class CreateDeviceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Device: Create")
            .WithSummary("Creates a new device")
            .WithDescription("Creates a new device")
            .WithOrder(1)
            .Produces<Response<Device?>>();
    
    private static async Task<IResult> HandleAsync(CreateDeviceRequest request, IDeviceHandler handler)
    {
        var result = await handler.CreateAsync(request); 
        return result.IsSuccess
            ? Results.Created($"/{result.Data?.Id}", result)
            : Results.BadRequest(result);
    }
}