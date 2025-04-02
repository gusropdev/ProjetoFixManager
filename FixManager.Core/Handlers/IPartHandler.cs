using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;

namespace FixManager.Core.Handlers;

public interface IPartHandler
{
    Task<Response<Part?>> CreateAsync(CreatePartRequest request);
    Task<Response<Part?>> UpdateAsync(UpdatePartRequest request);
    Task<Response<Part?>> DeleteAsync(DeletePartRequest request);
    Task<Response<Part?>> GetByIdAsync(GetPartByIdRequest request);
    Task<PagedResponse<List<Part>>> GetAllAsync(GetAllPartsRequest request);
}