using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.ServiceOrders;

public class UpdateServiceOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("ServiceOrder: Update")
            .WithSummary("Updates a service order")
            .WithDescription("Updates a service order")
            .WithOrder(2)
            .Produces<Response<ServiceOrder?>>();
    private static async Task<IResult> HandleAsync(UpdateServiceOrderRequest request, IServiceOrderHandler handler, int id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}