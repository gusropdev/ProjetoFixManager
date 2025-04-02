using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;

namespace FixManager.Core.Handlers;

public interface ICustomerHandler
{
    Task<Response<Customer?>> CreateAsync(CreateCustomerRequest request);
    Task<Response<Customer?>> UpdateAsync(UpdateCustomerRequest request);
    Task<Response<Customer?>> DeleteAsync(DeleteCustomerRequest request);
    Task<Response<Customer?>> GetByIdAsync(GetCustomerByIdRequest request);
    Task<PagedResponse<List<Customer>>> GetAllAsync(GetAllCustomersRequest request);
}