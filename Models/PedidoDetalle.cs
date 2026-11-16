namespace AgroManager.Models;

public class PedidoDetalle
{
    public int Id { get; set; }

    public  int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public  int  ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public  int Cantidad {get; set;}

    public  decimal PrecioUnitario{get; set;}

    public ICollection<MovimientoInventario> MovimientoInventarios {get; set;} = new List<MovimientoInventario>();
}