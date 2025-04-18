using System.ComponentModel.DataAnnotations;
using FixManager.Core.Requests.Devices;

namespace FixManager.Api.Services;

public abstract class DeviceService
{
    public static List<string> ValidateCreation(CreateDeviceRequest request)
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
    
    public static List<string> ValidateUpdate(UpdateDeviceRequest request)
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