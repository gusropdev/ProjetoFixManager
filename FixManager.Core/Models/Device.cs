using System.Text.Json.Serialization;

namespace FixManager.Core.Models;

public class Device
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; }  = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;

    public int ServiceOrderId { get; set; }
    
    [JsonIgnore]
    public ServiceOrder ServiceOrder { get; set; } = null!;
}