using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Customers;

public class CreateCustomerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Customer: Create")
            .WithSummary("Creates a new customer")
            .WithDescription("Creates a new customer")
            .WithOrder(1)
            .Produces<Response<Customer?>>();

    private static async Task<IResult> HandleAsync(CreateCustomerRequest request, ICustomerHandler handler)
    {
        var result = await handler.CreateAsync(request); 
        return result.IsSuccess
            ? Results.Created($"/{result.Data?.Id}", result)
            : Results.BadRequest(result);
    }
}