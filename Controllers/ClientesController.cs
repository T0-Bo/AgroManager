using AgroManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using AgroManager.Models;
using AgroManager.Data;
using Microsoft.EntityFrameworkCore;
using AgroManager.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace AgroManager.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListClientes()
    {
        var clientes  = await db.Clientes
        .Select(c => new ClienteResponse(c))
        .AsNoTracking()
        .ToListAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientesID(int id)
    {
        var cliente = await db.Clientes
        .Where(c => c.Id == id)
        .Select(c => new ClienteResponse(c))
        .FirstOrDefaultAsync();

        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost]

    public async Task<IActionResult> CreateClientes([FromBody] ClienteRequest request)
    {

         var cliente = new Cliente
        {
            Name = request.Name,
            Documento = request.Documento,
            Telefono = request.Telefono,
            Email = request.Email,
            Direccion = request.Direccion,
            Activo = request.Activo


        };

        db.Clientes.Add(cliente);
        await db.SaveChangesAsync();

       

         return ApiResponse.Created(new ClienteResponse(cliente), "cliente creado", $"/api/clientes/{cliente.Id}");
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> ActualizarCliente([FromBody] ClienteRequest request, int id)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (cliente is null)
        {
            return ApiResponse.NotFound("El cliente no existe");
        }

            cliente.Name = request.Name;
            cliente.Documento = request.Documento;
            cliente.Telefono = request.Telefono;
            cliente.Email = request.Email;
            cliente.Direccion = request.Direccion;
            cliente.Activo = request.Activo;

        await db.SaveChangesAsync();

        return ApiResponse.Ok(new ClienteResponse(cliente),"cliente actualizado");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EliminarCliente( int id)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (cliente is null)
        {
            return  ApiResponse.NotFound("El cliente no existe");
        }
        db.Clientes.Remove(cliente);
        
        await db.SaveChangesAsync();

        return NoContent();
    }
}