using System.ComponentModel.DataAnnotations;

namespace FixManager.Core.Requests.Devices;

public class UpdateDeviceRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public string Type { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    public string Brand { get; set; } = string.Empty;
    
    public string Model { get; set; }  = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    
}