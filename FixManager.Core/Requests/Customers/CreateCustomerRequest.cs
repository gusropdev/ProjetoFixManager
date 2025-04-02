using System.ComponentModel.DataAnnotations;

namespace FixManager.Core.Requests.Customers;

public class CreateCustomerRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
    [MaxLength(60, ErrorMessage = "O nome deve ter no máximo 60 caracteres.")]
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}