using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Devices;

public class UpdateDeviceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Devices: Update")
            .WithSummary("Updates a device")
            .WithDescription("Updates a device")
            .WithOrder(2)
            .Produces<Response<Device?>>();
    
    private static async Task<IResult> HandleAsync(UpdateDeviceRequest request, IDeviceHandler handler, int id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request); 
        
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}