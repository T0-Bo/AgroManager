using AgroManager.Models;

namespace AgroManager.DTOs;

public class PedidoDetalleResponse
{
    public int Id { get; set; }

    public  int PedidoId { get; set; }
    public int ProductoId { get; set; }
    public string? ProductoName { get; set; }

    public  decimal PrecioUnitario {get; set;}

    public  decimal Cantidad {get; set;}
    public decimal Subtotal => PrecioUnitario * Cantidad;

    public PedidoDetalleResponse(PedidoDetalle pedidoDetalle)
    {
        Id = pedidoDetalle.Id;
        PedidoId = pedidoDetalle.PedidoId;
        ProductoId = pedidoDetalle.ProductId;
        ProductoName = pedidoDetalle.Product?.Name;
        PrecioUnitario = pedidoDetalle.PrecioUnitario;
        Cantidad = pedidoDetalle.Cantidad;
    }
}