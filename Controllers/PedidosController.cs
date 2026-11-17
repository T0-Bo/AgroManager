
using AgroManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using AgroManager.Models;
using AgroManager.Data;
using Microsoft.EntityFrameworkCore;
using AgroManager.Enums;
using AgroManager.Helpers;

namespace AgroManager.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PedidosController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListPedidos()
    {
        var pedidos = await db.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.PedidoDetalles)
                .ThenInclude(pd => pd.Product)
            .AsNoTracking()
            .ToListAsync();

        var response = pedidos
            .Select(p => new PedidoResponse(p))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPedido(int id)
    {
        var pedido = await db.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.PedidoDetalles)
                .ThenInclude(pd => pd.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null)
        {
            return NotFound();
        }

        var response = new PedidoResponse(pedido);

        return Ok(response);
    }

    [HttpPost]

    public async Task<IActionResult> CreatePedido([FromBody] PedidoRequest request)
    {
        

        var clienteExists = await db.Clientes.AnyAsync(c => c.Id == request.ClienteId);
        if (!clienteExists)
        {
            return ApiResponse.BadRequest("El cliente no existe");
        }
        
        var productIds = request.Detalles
            .Select(d => d.ProductoId)
            .Distinct()
            .ToList();

        var productos = await db.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        if (productIds.Count != productos.Count)
        {
            return ApiResponse.BadRequest("Uno o más productos no existen");
        }
        if (productIds.Count != request.Detalles.Count)
        {
            return ApiResponse.BadRequest("No se permiten productos repetidos en el pedido.");
        }

        var productosPorId = productos.ToDictionary(p => p.Id);

        var pedido = new Pedido
        {
            ClienteId = request.ClienteId,
            Fecha = request.Fecha,
            Estado = EstadoPedido.Pendiente,
            MetodoPago = request.MetodoPago,
            PedidoDetalles = request.Detalles.Select(d => new PedidoDetalle
            {
                ProductId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = productosPorId[d.ProductoId].Price
            }).ToList()
        };

        db.Pedidos.Add(pedido);
        await db.SaveChangesAsync();

        var pedidoCreado = await db.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.PedidoDetalles)
                .ThenInclude(pd => pd.Product)
            .FirstAsync(p => p.Id == pedido.Id);

        return ApiResponse.Created(new PedidoResponse(pedidoCreado), "Pedido creado", $"/api/pedidos/{pedido.Id}");
    }
}
