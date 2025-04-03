using System.Text.Json.Serialization;
using FixManager.Core.Enums;

namespace FixManager.Core.Models;

public class ServiceOrder
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ServiceOrderStatus Status { get; set; } = ServiceOrderStatus.EmAnalise;
    public string ReportedIssue { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public decimal? EstimatedCost { get; set; }

    public int CustomerId { get; set; }
    
    [JsonIgnore]
    public Customer Customer { get; set; } = null!;
    
    public Device? Device { get; set; }
    public List<Part> Parts { get; set; } = [];
}