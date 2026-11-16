using System.ComponentModel.DataAnnotations;

namespace AgroManager.DTOs;

public class ClienteRequest
{
     [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 160 caracteres")]
    [Display(Name = "Nombre")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El documento es obligatorio")]
    [StringLength(13, MinimumLength = 6, ErrorMessage = "Documento inválido")]
    [RegularExpression(@"^\d{6,13}$", ErrorMessage = "El documento debe ser numérico")]
    [Display(Name = "Documento")]
    public required string Documento { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "Formato de teléfono inválido")]
    [Display(Name = "Teléfono")]
    public required string Telefono { get; set; }

    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 200 caracteres")]
    [Display(Name = "Dirección")]
    public required string Direccion { get; set; }

    [Display(Name = "Activo")]
    public bool Activo { get; set; }
}
