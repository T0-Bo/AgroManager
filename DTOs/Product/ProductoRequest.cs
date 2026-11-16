using System.ComponentModel.DataAnnotations;
using AgroManager.Enums;

namespace AgroManager.DTOs;

public class ProductRequest
{
    [Required (ErrorMessage = "Name is required."), StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string? Name { get; set; }


    public string? Description { get; set; }

    [Required, Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal? Price { get; set; }


    [Required, EnumDataType(typeof(UnidadMedida), ErrorMessage = "Unit must be a valid UnidadMedida.")]
    public UnidadMedida? Unit { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int? CategoryId { get; set; }

    public int? MarcaId { get; set; }   
    


}
