using System.ComponentModel.DataAnnotations;
using FixManager.Core.Requests.ServiceOrders;

namespace FixManager.Api.Services;

public abstract class ServiceOrderService
{
    public static List<string> ValidateCreation(CreateServiceOrderRequest request)
    {
        var errors = new List<string>();
        var validationContext = new ValidationContext(request);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(request, validationContext, validationResults, true))
        {
            errors = validationResults
                .Select(v => v.ErrorMessage ?? "Unknown validation error")
                .ToList();
        }
        
        return errors;
    }
}