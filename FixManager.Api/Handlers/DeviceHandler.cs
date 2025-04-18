using System.ComponentModel.DataAnnotations;
using FixManager.Api.Data;
using FixManager.Api.Services;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Devices;
using FixManager.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Handlers;

public class DeviceHandler (AppDbContext context) : IDeviceHandler
{
    public async Task<Response<Device?>> CreateAsync(CreateDeviceRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var errors = DeviceService.ValidateCreation(request);
        if (errors.Count != 0)
            return new Response<Device?>(null, 400, "Validation failed", errors);

        var existingServiceOrder = await context.ServiceOrders
            .Include(so => so.Device)
            .FirstOrDefaultAsync(so => so.Id == request.ServiceOrderId);
        
        //Validando os dados no banco
        //Validando se o ServiceOrder informado é existente
        if (existingServiceOrder == null)
            return new Response<Device?>(null, 404, "Service order not found", null);
        
        //Validando se já há um Device associado ao ServiceOrder
        if(existingServiceOrder.Device != null)
            return new Response<Device?>(existingServiceOrder.Device, 404, "This device order already has a device assigned", null);

        try
        {
            var device = new Device
            {
                Type = request.Type,
                Brand = request.Brand,
                Model = request.Model,
                SerialNumber = request.SerialNumber,
                ServiceOrderId = request.ServiceOrderId
            };
            
            await context.Devices.AddAsync(device);
            await context.SaveChangesAsync();
            
            return new Response<Device?>(device, 201, "Device created successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Device?>(null, 500, $"Device could not be created. Error: {ex.Message}");
        }
    }
    
    public async Task<Response<Device?>> UpdateAsync(UpdateDeviceRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var errors = DeviceService.ValidateUpdate(request);
        if (errors.Count != 0)
            return new Response<Device?>(null, 400, "Validation failed", errors);
        
        try
        {
            var device = await context.Devices.FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (device == null)
                return new Response<Device?>(null, 404, "Device not found.");
            
            device.Type = request.Type;
            device.Brand = request.Brand;
            device.Model = request.Model;
            device.SerialNumber = request.SerialNumber;

            context.Devices.Update(device);
            await context.SaveChangesAsync();
            
            return new Response<Device?>(device, message: "Device updated successfully.");
        }
        catch (Exception ex)
        {
            return new  Response<Device?>(null, 500, $"Device could not be updated. Error: {ex.Message}");
        }
    }

    public async Task<Response<Device?>> DeleteAsync(DeleteDeviceRequest request)
    {
        try
        {
            var device = await context.Devices.FirstOrDefaultAsync(x=>x.Id == request.Id);
            if (device == null)
                return new Response<Device?>(null, 404, "Device not found.");
            
            context.Devices.Remove(device);
            await context.SaveChangesAsync();
            
            return new Response<Device?>(device, message: "Device deleted successfully.");
        }
        catch (Exception ex)
        {
            return new  Response<Device?>(null, 500, $"Device could not be deleted. Error: {ex.Message}");
        }
    }

    public async Task<Response<Device?>> GetByIdAsync(GetDeviceByIdRequest request)
    {
        try
        {
            var device = await context.Devices.FirstOrDefaultAsync(x=>x.Id == request.Id);
            return device == null 
                ? new Response<Device?>(null, 404, "Device not found.") 
                : new Response<Device?>(device, message: "Device retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new  Response<Device?>(null, 500, $"Device could not be retrieved. Error: {ex.Message}");
        }
    }

    public async Task<PagedResponse<List<Device>>> GetAllAsync(GetAllDevicesRequest request)
    {
        try
        {
            var query = context.Devices
                .AsNoTracking()
                .OrderBy(x => x.ServiceOrderId);
            
            var devices = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Device>>(devices, count, request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Device>>(null, 500, $"Devices could not be retrieved. Error: {ex.Message}");
        }
    }
}