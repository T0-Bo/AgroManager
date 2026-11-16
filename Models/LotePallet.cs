namespace AgroManager.Models;

public class LotePallet
{
    public int Id { get; set;}

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public required string CodigoPallet { get; set;}
    public int CantidadInicial { get; set; }
    public int CantidadActual { get; set; }

    public DateTime FechaIngreso { get; set; }
    public DateTime? FechaVencimiento { get; set; }

    public string? UbicacionAlmacen {get; set;}

    public ICollection<MovimientoInventario> MovimientoInventarios {get; set;} = new List<MovimientoInventario>();
}