using FixManager.Api.Common.Api;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;

namespace FixManager.Api.Endpoints.Parts;

public class UpdatePartEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:int}", HandleAsync)
            .WithName("Part: Update")
            .WithSummary("Updates a part")
            .WithDescription("Updates a part")
            .WithOrder(2)
            .Produces<Response<Part?>>();
    
    private static async Task<IResult> HandleAsync(UpdatePartRequest request, IPartHandler handler, int id)
    {
        request.Id = id;
        
        var result = await handler.UpdateAsync(request); 
        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}