using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;

namespace FixManager.Core.Handlers;

public interface IDeviceHandler
{
    Task<Response<Device?>> CreateAsync(CreateDeviceRequest request);
    Task<Response<Device?>> UpdateAsync(UpdateDeviceRequest request);
    Task<Response<Device?>> DeleteAsync(DeleteDeviceRequest request);
    Task<Response<Device?>> GetByIdAsync(GetDeviceByIdRequest request);
    Task<PagedResponse<List<Device>>> GetAllAsync(GetAllDevicesRequest request);
}