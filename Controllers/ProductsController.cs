using AgroManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using AgroManager.Models;
using AgroManager.Data;
using Microsoft.EntityFrameworkCore;
using AgroManager.Helpers;

namespace AgroManager.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListProducts()
    {
        var products = await db.Products
            .Select(p => new ProductoResponse(p)
            {
                MarcaName = p.Marca != null ? p.Marca.Name : null,
                CategoryName = p.Category != null ? p.Category.Name : null,
                Stock = p.LotePallets.Sum(lp => lp.CantidadActual)
            })
            .AsNoTracking()
            .ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductID(int id)
    {
        var product = await db.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductoResponse(p)
            {
                MarcaName = p.Marca != null ? p.Marca.Name : null,
                CategoryName = p.Category != null ? p.Category.Name : null,
                Stock = p.LotePallets.Sum(lp => lp.CantidadActual)
            })
            .FirstOrDefaultAsync();

        return product is null ? ApiResponse.NotFound("El producto no existe") : Ok(product);
    }

    [HttpPost]

    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {

        bool existeCategories = await db.Categories
            .AnyAsync(c => c.Id == request.CategoryId);

        if (!existeCategories)
        {
            return ApiResponse.BadRequest("La categoría no existe");
        }
        if (request.MarcaId.HasValue)
        {
            bool existeMarcas = await db.Marcas
            .AnyAsync(m => m.Id == request.MarcaId);
        
            if (!existeMarcas)
            {
                return ApiResponse.BadRequest("La Marca no existe");
            }
        }



        var product = new Product
        {
            Name = request.Name!,
            Description = request.Description,
            Price = request.Price!.Value,
            Unit = request.Unit!.Value,
            CategoryId = request.CategoryId!.Value,
            MarcaId = request.MarcaId
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

         return ApiResponse.Created(new ProductoResponse(product), "Producto creado", $"/api/products/{product.Id}");
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> ActualizarProduct([FromBody] ProductRequest request, int id)
    {
        var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            return ApiResponse.NotFound("El producto no existe");
        }

        bool existeCategories = await db.Categories
            .AnyAsync(c => c.Id == request.CategoryId);

        if (!existeCategories)
        {
            return ApiResponse.BadRequest("La categoría no existe");
        }
        if (request.MarcaId.HasValue)
        {
            bool existeMarcas = await db.Marcas
            .AnyAsync(m => m.Id == request.MarcaId);
        
            if (!existeMarcas)
            {
                return ApiResponse.BadRequest("La Marca no existe");
            }
        }

        product.Name = request.Name!;
        product.Description = request.Description;
        product.Price = request.Price!.Value;
        product.Unit = request.Unit!.Value;
        product.CategoryId = request.CategoryId!.Value;
        product.MarcaId = request.MarcaId;

        await db.SaveChangesAsync();

        return ApiResponse.Ok(new ProductoResponse(product),"Producto actualizado");
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> EliminarProduct( int id)
    {
        var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            return  ApiResponse.NotFound("El producto no existe");
        }
        db.Products.Remove(product);
        
        await db.SaveChangesAsync();

        return NoContent();
    }
}