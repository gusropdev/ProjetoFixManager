using System.ComponentModel.DataAnnotations;

namespace FixManager.Core.Requests.Devices;

public class CreateDeviceRequest
{
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public string Type { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    public string Brand { get; set; } = string.Empty;
    
    public string Model { get; set; }  = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = " A ordem de serviço é obrigatória.")]
    public int ServiceOrderId { get; set; }
}