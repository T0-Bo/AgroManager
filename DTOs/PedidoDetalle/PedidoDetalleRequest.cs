using System.ComponentModel.DataAnnotations;

namespace AgroManager.DTOs;

public class PedidoDetalleRequest
{
    [Range(1, int.MaxValue)]
    public int ProductoId { get; set; }

    [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
        ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public decimal Cantidad { get; set; }
}