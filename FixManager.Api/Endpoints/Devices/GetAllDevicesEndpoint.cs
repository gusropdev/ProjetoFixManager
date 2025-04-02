using FixManager.Api.Common.Api;
using FixManager.Core;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FixManager.Api.Endpoints.Devices;

public class GetAllDevicesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Devices: Get all")
            .WithSummary("Retrieves all devices")
            .WithDescription("Retrieves all devices")
            .WithOrder(5)
            .Produces<Response<Device?>>();
    
    private static async Task<IResult> HandleAsync(
        IDeviceHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllDevicesRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllAsync(request); 
        return result.IsSuccess
            ? Results.Ok( result)
            : Results.BadRequest(result);
    }
}