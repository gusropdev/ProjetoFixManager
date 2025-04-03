using System.ComponentModel.DataAnnotations;
using FixManager.Core.Enums;
using FixManager.Core.Models;

namespace FixManager.Core.Requests.ServiceOrders;

public class UpdateServiceOrderRequest
{
    public int Id { get; set; }
    
    [EnumDataType(typeof(ServiceOrderStatus))]
    public ServiceOrderStatus Status { get; set; }
    
    [Required(ErrorMessage = "O problema relatado é obrigatório.")]
    [MaxLength(ErrorMessage = "O problema relatado deve ter no máximo 255 caracteres.")]
    public string ReportedIssue { get; set; } = string.Empty;
    
    [MaxLength(ErrorMessage = "O tamanho máximo do diagnóstico é de 255 caraceteres.")]
    public string Diagnosis { get; set; } = string.Empty;
    
    [DataType(DataType.Currency)]
    public decimal? EstimatedCost { get; set; } = null;
}