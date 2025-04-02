using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Customers;

public class GetCostumerByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Customers: Get by Id")
            .WithSummary("Retrieves a customer")
            .WithDescription("Retrieves a costumer")
            .WithOrder(4)
            .Produces<Response<Customer?>>();

    private static async Task<IResult> HandleAsync(ICustomerHandler handler, int id)
    {
        var request = new GetCustomerByIdRequest
        {
            Id = id
        };

        var result = await handler.GetByIdAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}