using AgroManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using AgroManager.Models;
using AgroManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AgroManager.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListCategories()
    {
        var categories  = await db.Categories
        .Select(c => new CategorieResponse(c))
        .AsNoTracking()
        .ToListAsync();

        return Ok(categories );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryID(int id)
    {
        var category = await db.Categories
        .Where(c => c.Id == id)
        .Select(c => new CategorieResponse(c))
        .FirstOrDefaultAsync();

        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategorie([FromBody] CategorieRequest request)
    {

        var category = new Category
        {
            Name = request.Name!,
        };

        db.Categories.Add(category);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoryID), new { id = category.Id }, new CategorieResponse(category));
    }
}
