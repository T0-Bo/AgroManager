using System.ComponentModel.DataAnnotations;

namespace AgroManager.DTOs;

public class CategorieRequest
{
    [Required (ErrorMessage = "Name is required."), StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string? Name { get; set; }
}
