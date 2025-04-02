using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Parts;

public class GetPartByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:int}", HandleAsync)
            .WithName("Part: Get by id")
            .WithSummary("Retrieves a part")
            .WithDescription("Retrieves a  part")
            .WithOrder(4)
            .Produces<Response<Part?>>();
    
    private static async Task<IResult> HandleAsync(IPartHandler handler, int id)
    {
        var request = new GetPartByIdRequest
        {
            Id = id
        };
        
        var result = await handler.GetByIdAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}