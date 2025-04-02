using System.ComponentModel.DataAnnotations;

namespace FixManager.Core.Requests.Customers;

public class UpdateCustomerRequest
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [Length(3, 60, ErrorMessage = "O nome deve ter entre 3 e 60 caracteres.")]
    public string Name { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
}