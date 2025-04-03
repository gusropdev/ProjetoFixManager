using System.ComponentModel.DataAnnotations;
using FixManager.Core.Requests.Customers;

namespace FixManager.Api.Services;

public abstract class CustomerService
{
    public static List<string> ValidateCreation(CreateCustomerRequest request)
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
    
    public static List<string> ValidateUpdate(UpdateCustomerRequest request)
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