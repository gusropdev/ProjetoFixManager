using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;

namespace FixManager.Core.Handlers;

public interface IServiceOrderHandler
{
    Task<Response<ServiceOrder?>> CreateAsync(CreateServiceOrderRequest request);
    Task<Response<ServiceOrder?>> UpdateAsync(UpdateServiceOrderRequest request);
    Task<Response<ServiceOrder?>> DeleteAsync(DeleteServiceOrderRequest request);
    Task<Response<ServiceOrder?>> GetByIdAsync(GetServiceOrderByIdRequest request);
    Task<PagedResponse<List<ServiceOrder>>> GetAllAsync(GetAllServiceOrdersRequest request);
}