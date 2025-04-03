using System.ComponentModel.DataAnnotations;
using FixManager.Api.Data;
using FixManager.Api.Services;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.ServiceOrders;
using FixManager.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Handlers;

public class ServiceOrderHandler (AppDbContext context) : IServiceOrderHandler
{
    public async Task<Response<ServiceOrder?>> CreateAsync(CreateServiceOrderRequest request)
    {
        //Validando a criação da ServiceOrder
        var errors = ServiceOrderService.ValidateCreation(request);
        if (errors.Count != 0)
            return new Response<ServiceOrder?>(null, 400, "Validation failed", errors);
        
        var existingCustomer = await context.Customers.FindAsync(request.CustomerId);
        if (existingCustomer == null)
        {
            return new Response<ServiceOrder?>(null, 404, "Customer not found.");
        }
 
        try
        {
            var serviceOrder = new ServiceOrder
            {
                ReportedIssue = request.ReportedIssue,
                CustomerId = request.CustomerId
            };
            
            await context.ServiceOrders.AddAsync(serviceOrder);
            await context.SaveChangesAsync();
            
            return new Response<ServiceOrder?>(serviceOrder, 201, "Service order created successfully.");
        }
        catch (Exception ex)
        {
            return new Response<ServiceOrder?>(null, 500, $"Service order could not be created. Error: {ex.Message}");
        }
    }

    public async Task<Response<ServiceOrder?>> UpdateAsync(UpdateServiceOrderRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var errors = ServiceOrderService.ValidateUpdate(request);
        if (errors.Count != 0)
            return new Response<ServiceOrder?>(null, 400, "Validation failed", errors);
        
        try
        {
            var serviceOrder = await context.ServiceOrders.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (serviceOrder == null)
                return new Response<ServiceOrder?>(null, 404, "Service order could not be found.");
            
            serviceOrder.ReportedIssue = request.ReportedIssue;
            serviceOrder.Diagnosis = request.Diagnosis;
            serviceOrder.EstimatedCost = request.EstimatedCost;
            serviceOrder.Status = request.Status;
            
            context.ServiceOrders.Update(serviceOrder);
            await context.SaveChangesAsync();
            
            return new Response<ServiceOrder?>(serviceOrder, message: "Service order updated successfully.");
        }
        catch (Exception ex)
        {
            return new Response<ServiceOrder?>(null, 500, $"Service order could not be updated. Error: {ex.Message}");
        }
    }

    public async Task<Response<ServiceOrder?>> DeleteAsync(DeleteServiceOrderRequest request)
    {
        try
        {
            var serviceOrder = await context.ServiceOrders.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (serviceOrder == null)
                return new Response<ServiceOrder?>(null, 404, "Service order could not be found.");
            
            context.ServiceOrders.Remove(serviceOrder);
            await context.SaveChangesAsync();
            
            return new Response<ServiceOrder?>(serviceOrder, message: "Service order deleted successfully.");
        }
        catch (Exception ex)
        {
            return new Response<ServiceOrder?>(null, 500, $"Service order could not be deleted. Error: {ex.Message}");
        }
    }

    public async Task<Response<ServiceOrder?>> GetByIdAsync(GetServiceOrderByIdRequest request)
    {
        try
        {
            var serviceOrder = await context.ServiceOrders
                .Include(so => so.Device)
                .Include(so => so.Parts)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            return serviceOrder == null 
                ? new Response<ServiceOrder?>(null, 404, "Service order could not be found.") 
                : new Response<ServiceOrder?>(serviceOrder, message: "Service order retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new Response<ServiceOrder?>(null, 500, $"Service order could not be updated. Error: {ex.Message}");
        }
    }

    public async Task<PagedResponse<List<ServiceOrder>>> GetAllAsync(GetAllServiceOrdersRequest request)
    {
        try
        {
            var query = context.ServiceOrders
                .AsNoTracking()
                .OrderBy(x=> x.CreatedAt);
        
            var serviceOrders = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
            
            return new PagedResponse<List<ServiceOrder>>(serviceOrders, count, request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<ServiceOrder>>(null, 500, $"Service orders could not be retrieved. Error: {ex.Message}");
        }

    }
}