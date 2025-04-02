using System.Text.Json.Serialization;

namespace FixManager.Core.Models;

public class Part
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public int ServiceOrderId { get; set; }
    
    [JsonIgnore]
    public ServiceOrder? ServiceOrder { get; set; }
}