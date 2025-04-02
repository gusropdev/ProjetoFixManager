using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Devices;

public class GetDeviceByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Devices: Get by id")
            .WithSummary("Retrieves a device")
            .WithDescription("Retrieves a device")
            .WithOrder(4)
            .Produces<Response<Device?>>();
    
    private static async Task<IResult> HandleAsync(IDeviceHandler handler, int id)
    {
        var request =  new GetDeviceByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}