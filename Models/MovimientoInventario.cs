using AgroManager.Enums;

namespace AgroManager.Models;

public class MovimientoInventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public Product Product { get; set; } = null!;

    public TipoMovimientoInventario Tipo { get; set; }
    public decimal Cantidad { get; set; }

    public required DateTime Fecha { get; set; }
    public required string Motivo { get; set; }

    public int LotePalletId { get; set; }
    public LotePallet LotePallet { get; set; } = null!;

    public int? PedidoDetalleId { get; set; }
    public PedidoDetalle? PedidoDetalle { get; set; }

    public int? EntregaProgramadaId { get; set; }
    public EntregaProgramada? EntregaProgramada { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}