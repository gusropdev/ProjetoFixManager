using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.ServiceOrders;

public class DeleteServiceOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("ServiceOrder: Delete")
            .WithSummary("Deletes a service order")
            .WithDescription("Deletes a service order")
            .WithOrder(3)
            .Produces<Response<ServiceOrder?>>();
    
    private static async Task<IResult> HandleAsync(IServiceOrderHandler handler, int id)
    {
        var request = new DeleteServiceOrderRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}