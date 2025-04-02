using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.ServiceOrders;

public class GetServiceOrderByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("ServiceOrder: GetById")
            .WithSummary("Retrieves a service order")
            .WithDescription("Retrieves a service order")
            .WithOrder(4)
            .Produces<Response<ServiceOrder?>>();

    private static async Task<IResult> HandleAsync(IServiceOrderHandler handler, int id)
    {
        var request = new GetServiceOrderByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}