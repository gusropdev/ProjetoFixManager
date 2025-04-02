using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;


namespace FixManager.Api.Endpoints.Customers;

public class DeleteCostumerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("Customers: Delete")
            .WithSummary("Delete a customer")
            .WithDescription("Delete a costumer")
            .WithOrder(3)
            .Produces<Response<Customer?>>();

    private static async Task<IResult> HandleAsync(ICustomerHandler handler, int id)
    {
        var request = new DeleteCustomerRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}