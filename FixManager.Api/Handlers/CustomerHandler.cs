using System.ComponentModel.DataAnnotations;
using FixManager.Api.Data;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Customers;
using FixManager.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Handlers;

public class CustomerHandler(AppDbContext context) : ICustomerHandler
{
    public async Task<Response<Customer?>> CreateAsync(CreateCustomerRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            var errorMessages = validationResults
                .Select(v => v.ErrorMessage ?? "Unknown validation error")
                .ToList();

            return new Response<Customer?>(null, 400, "Validation failed.", errorMessages);
        }

        try
        {
            var customer = new Customer
            {
                Name = request.Name,
                Phone = request.Phone,
            };
            
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            
            return new Response<Customer?>(customer, 201, "Customer created successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Customer?>(null, 500, $"Customer could not be created. Error: {ex.Message}");
        }
    }

    public async Task<Response<Customer?>> UpdateAsync(UpdateCustomerRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            var errorMessages = validationResults
                .Select(v => v.ErrorMessage ?? "Unknown validation error")
                .ToList();

            return new Response<Customer?>(null, 400, "Validation failed.", errorMessages);
        }
        
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (customer == null)
                return new Response<Customer?>(null, 404, "Customer could not be found.");
            
            customer.Name = request.Name;
            customer.Phone = request.Phone;

            context.Customers.Update(customer);
            await context.SaveChangesAsync();
            
            return new Response<Customer?>(customer ,message: "Customer updated successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Customer?>(null, 500, $"Customer could not be updated. Error: {ex.Message}");
        }
    }

    public async Task<Response<Customer?>> DeleteAsync(DeleteCustomerRequest request)
    {
        try
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (customer == null)
                return new Response<Customer?>(null, 404, "Customer could not be found.");
            
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
            
            return new Response<Customer?>(customer ,message: "Customer deleted successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Customer?>(null, 500, $"Customer could not be deleted. Error: {ex.Message}");
        }
    }

    public async Task<Response<Customer?>> GetByIdAsync(GetCustomerByIdRequest request)
    {
        try
        {
            var customer = await context.Customers
                .AsNoTracking()
                .Include(c => c.ServiceOrders)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
             return customer == null 
                ? new Response<Customer?>(null, 404, "Customer could not be found.") 
                : new Response<Customer?>(customer ,message: "Customer retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Customer?>(null, 500, $"Customer could not be retrieved. Error: {ex.Message}");
        }
    }

    public async Task<PagedResponse<List<Customer>>> GetAllAsync(GetAllCustomersRequest request)
    {
        try
        {
            var query = context.Customers
                .AsNoTracking()
                .Include(c => c.ServiceOrders)
                .OrderBy(c => c.Name);
            
            var customers = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Customer>>(customers, count, request.PageNumber, request.PageSize);
            
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Customer>>(null, 500, $"Costumers could not be retrieved. Error: {ex.Message}");
        }
    }
}