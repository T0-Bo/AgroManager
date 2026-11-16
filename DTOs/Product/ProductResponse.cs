using AgroManager.Models;
using AgroManager.Enums;
namespace AgroManager.DTOs;

public class ProductoResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public UnidadMedida Unit { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int? MarcaId { get; set; }
    public string? MarcaName { get; set; }  // ← solo un string, no la entidad

    public ProductoResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Unit = product.Unit;
        CategoryId = product.CategoryId;
        MarcaId = product.MarcaId;
        // MarcaNombre se setea desde la query, no acá
    }
}   