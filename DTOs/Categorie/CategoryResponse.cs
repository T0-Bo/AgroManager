using AgroManager.Models;

namespace AgroManager.DTOs;

public class CategorieResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;


    public CategorieResponse(Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }
}   