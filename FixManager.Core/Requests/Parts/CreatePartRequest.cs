using System.ComponentModel.DataAnnotations;

namespace FixManager.Core.Requests.Parts;

public class CreatePartRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [Length(3, 60, ErrorMessage = "O nome deve ter entre 3 e 60 caracteres.")]
    public string Name { get; set; } = string.Empty;
    
    [DataType(DataType.Currency)]
    public decimal? Price { get; set; }
    
    [Required(ErrorMessage = "A ordem de serviço é obrigatória.")]
    public int ServiceOrderId { get; set; }
}