using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Parts;

public class DeletePartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:int}", HandleAsync)
            .WithName("Part: Delete")
            .WithSummary("Deletes a part")
            .WithDescription("Deletes a part")
            .WithOrder(3)
            .Produces<Response<Part?>>();
    
    private static async Task<IResult> HandleAsync(IPartHandler handler, int id)
    {
        var request = new DeletePartRequest
        {
            Id = id
        };
        
        var result = await handler.DeleteAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}