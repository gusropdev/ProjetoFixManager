using System.ComponentModel.DataAnnotations;
using FixManager.Api.Data;
using FixManager.Core.Handlers;
using FixManager.Core.Models;
using FixManager.Core.Requests.Parts;
using FixManager.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FixManager.Api.Handlers;

public class PartHandler(AppDbContext context) : IPartHandler
{
    public async Task<Response<Part?>> CreateAsync(CreatePartRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            var errorMessages = validationResults
                .Select(v => v.ErrorMessage ?? "Unknown validation error")
                .ToList();

            return new Response<Part?>(null, 400, "Validation failed.", errorMessages);
        }

        try
        {
            var part = new Part
            {
                Name = request.Name,
                Price = request.Price,
                ServiceOrderId = request.ServiceOrderId
            };
            
            await context.Parts.AddAsync(part);
            await context.SaveChangesAsync();
            
            return new Response<Part?>(part, 201, "Part created successfully.");
        }
        catch (Exception ex)
        {
            return new Response<Part?>(null, 500, $"Part could not be created. Error: {ex.Message}");
        }
    }

    public async Task<Response<Part?>> UpdateAsync(UpdatePartRequest request)
    {
        //Validando os DataAnnotations declarados dentro do Request
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            var errorMessages = validationResults
                .Select(v => v.ErrorMessage ?? "Unknown validation error")
                .ToList();

            return new Response<Part?>(null, 400, "Validation failed.", errorMessages);
        }

        try
        {
            var part = await context.Parts.FirstOrDefaultAsync(x => x.Id == request.Id);
            if(part == null)
                return new Response<Part?>(null, 404, "Part could not be found.");
            
            part.Name = request.Name;
            part.Price = request.Price;
            
            context.Parts.Update(part);
            await context.SaveChangesAsync();
            
            return  new Response<Part?>(part, message: "Part updated successfully.");
            
        }
        catch (Exception ex)
        {
            return new  Response<Part?>(null, 500, $"Part could not be updated. Error: {ex.Message}");
        }
        
    }

    public async Task<Response<Part?>> DeleteAsync(DeletePartRequest request)
    {
        try
        {
            var part = await context.Parts.FirstOrDefaultAsync(x => x.Id == request.Id);
            if(part == null)
                return new Response<Part?>(null, 404, "Part could not be found.");
            
            context.Parts.Remove(part);
            await context.SaveChangesAsync();
            
            return  new Response<Part?>(part, message: "Part deleted successfully.");
            
        }
        catch (Exception ex)
        {
            return new  Response<Part?>(null, 500, $"Part could not be deleted. Error: {ex.Message}");
        }
    }

    public async Task<Response<Part?>> GetByIdAsync(GetPartByIdRequest request)
    {
        try
        {
            var part = await context.Parts.FirstOrDefaultAsync(x => x.Id == request.Id);
            return part == null 
            ? new Response<Part?>(null, 404, "Part could not be found.") 
            : new Response<Part?>(part, message: "Part retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new  Response<Part?>(null, 500, $"Part could not be retrieved. Error: {ex.Message}");
        }
    }

    public async Task<PagedResponse<List<Part>>> GetAllAsync(GetAllPartsRequest request)
    {
        try
        {
            var query = context.Parts
                .AsNoTracking()
                .OrderBy(x=> x.ServiceOrderId);
        
            var parts = await query
                .Skip((request.PageNumber -1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        
            var count = await query.CountAsync();
            
            return new PagedResponse<List<Part>>(parts, count, request.PageNumber, request.PageSize);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<Part>>(null, 500, $"Parts could not be retrieved. Error: {ex.Message}");
        }

    }
}