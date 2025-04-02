using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Devices;

public class DeleteDeviceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("Devices: Delete")
            .WithSummary("Deletes a device")
            .WithDescription("Deletes a  device")
            .WithOrder(3)
            .Produces<Response<Device?>>();
    
    private static async Task<IResult> HandleAsync(IDeviceHandler handler, int id)
    {
        var request =  new DeleteDeviceRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}