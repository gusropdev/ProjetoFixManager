using System.ComponentModel.DataAnnotations;
using FixManager.Core.Models;

namespace FixManager.Core.Requests.ServiceOrders;

public class CreateServiceOrderRequest
{
    [Required(ErrorMessage = "O problema relatado não pode estar vazio.")]
    [MaxLength(ErrorMessage = "O problema relatado deve ter no máximo 255 caracteres.")]
    public string ReportedIssue { get; set; } = string.Empty;

    [Required(ErrorMessage = "O cliente relacionado ao serviço é obrigatório.")]
    public int CustomerId { get; set; }
}