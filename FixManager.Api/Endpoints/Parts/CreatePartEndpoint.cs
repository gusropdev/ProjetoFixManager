using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Parts;

public class CreatePartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Part: Create")
            .WithSummary("Creates a new part")
            .WithDescription("Creates a new part")
            .WithOrder(1)
            .Produces<Response<Part?>>();
    
    private static async Task<IResult> HandleAsync(CreatePartRequest request, IPartHandler handler)
    {
        var result = await handler.CreateAsync(request); 
        return result.IsSuccess
            ? Results.Created($"/{result.Data?.Id}", result)
            : Results.BadRequest(result);
    }
}