using FixManager.Api.Common.Api;
using FixManager.Core;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FixManager.Api.Endpoints.Parts;

public class GetAllPartsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Parts: Get all")
            .WithSummary("Retrieves all parts")
            .WithDescription("Retrieves all parts")
            .WithOrder(5)
            .Produces<Response<Part?>>();
    
    private static async Task<IResult> HandleAsync(
        IPartHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllPartsRequest
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