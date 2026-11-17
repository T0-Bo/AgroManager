using AgroManager.Enums;

namespace AgroManager.Models;

public class EntregaProgramada
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public Product Product { get; set; } = null!;

    public int ProveedorId { get; set; }
    public Proveedor Proveedor { get; set; } = null!;

    public decimal CantidadEsperada { get; set; }
    public decimal CantidadRecibida { get; set; }

    public required DateTime FechaProgramada { get; set; }
    public DateTime? FechaRecepcion { get; set; }

    public EstadoEntregaProgramada Estado { get; set; }
}