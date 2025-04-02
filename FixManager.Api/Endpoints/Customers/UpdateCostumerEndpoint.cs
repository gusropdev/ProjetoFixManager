using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Customers;

public class UpdateCostumerEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Customers: Update")
            .WithSummary("Updates a customer")
            .WithDescription("Updates a costumer")
            .WithOrder(2)
            .Produces<Response<Customer?>>();

    private static async Task<IResult> HandleAsync(UpdateCustomerRequest request, ICustomerHandler handler, int id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}