using System.ComponentModel.DataAnnotations;
using FixManager.Core.Requests.Parts;

namespace FixManager.Api.Services;

public class PartService
{
    public static List<string> ValidateCreation(CreatePartRequest request)
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
    
    public static List<string> ValidateUpdate(UpdatePartRequest request)
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