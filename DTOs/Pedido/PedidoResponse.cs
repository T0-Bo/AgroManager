using AgroManager.Models;
using AgroManager.Enums;
namespace AgroManager.DTOs;

public class PedidoResponse
{
    public int Id { get; set; }

    public  int ClienteId { get; set; }
    public string? ClienteName { get; set; }

    public required DateTime Fecha {get; set;}

    public  EstadoPedido Estado {get; set;}

    public  MetodoPago MetodoPago {get; set;}

    public PedidoResponse(Pedido pedido)
    {
        Id = pedido.Id;
        ClienteId = pedido.ClienteId;
        ClienteName = pedido.Cliente?.Name;
        Fecha = pedido.Fecha;
        Estado = pedido.Estado;
        MetodoPago = pedido.MetodoPago;
    }
}