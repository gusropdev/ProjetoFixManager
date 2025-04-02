using FixManager.Api.Common.Api;
using FixManager.Core;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FixManager.Api.Endpoints.Customers;

public class GetAllCostumersEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Customers: Get all")
            .WithSummary("Retrieves all customers")
            .WithDescription("Retrieves all costumers")
            .WithOrder(5)
            .Produces<PagedResponse<List<Customer?>>>();

    private static async Task<IResult> HandleAsync(
        ICustomerHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCustomersRequest
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