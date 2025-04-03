using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace FixManager.Core.Requests.Parts;

public class CreatePartRequest
{
    [Required(ErrorMessage = "O nome da peça é obrigatório.")]
    [Length(3, 60, ErrorMessage = "O nome da peça deve ter entre 3 e 60 caracteres.")]
    public string Name { get; set; } = string.Empty;
    
    [DataType(DataType.Currency)]
    [Required(ErrorMessage = "O preço da peça é obrigatório.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "A ordem de serviço é obrigatória.")]
    public int ServiceOrderId { get; set; }
}