using FixManager.Api.Common.Api;
using FixManager.Core;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FixManager.Api.Endpoints.ServiceOrders;

public class GetAllServiceOrdersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("ServiceOrder: Get all")
            .WithSummary("Retrieves all service orders")
            .WithDescription("Retrieves all service orders")
            .WithOrder(5)
            .Produces<PagedResponse<List<ServiceOrder?>>>();

    private static async Task<IResult> HandleAsync(
        IServiceOrderHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllServiceOrdersRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllAsync(request);
        
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}