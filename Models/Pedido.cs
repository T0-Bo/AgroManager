using AgroManager.Enums;

namespace AgroManager.Models;

public class Pedido
{
    public int Id { get; set; }

    public  int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public required DateTime Fecha {get; set;}

    public  EstadoPedido Estado {get; set;}

    public  MetodoPago MetodoPago {get; set;}

    public ICollection<PedidoDetalle> PedidoDetalles {get; set;} = new List<PedidoDetalle>();
}