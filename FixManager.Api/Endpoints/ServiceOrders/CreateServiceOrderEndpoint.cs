using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.ServiceOrders;

public class CreateServiceOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("ServiceOrder: Create")
            .WithSummary("Creates a new service order")
            .WithDescription("Creates a new service order")
            .WithOrder(1)
            .Produces<Response<ServiceOrder?>>();

    private static async Task<IResult> HandleAsync(CreateServiceOrderRequest request, IServiceOrderHandler handler)
    {
        var result = await handler.CreateAsync(request); 
        return result.IsSuccess
            ? Results.Created($"/{result.Data?.Id}", result)
            : Results.BadRequest(result);
    }
}