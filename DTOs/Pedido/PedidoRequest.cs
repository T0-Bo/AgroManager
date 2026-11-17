
using System.ComponentModel.DataAnnotations;
using AgroManager.Enums;

namespace AgroManager.DTOs;

public class PedidoRequest
{
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [DataType(DataType.Date)]
    [DisplayFormat(
        DataFormatString = "{0:yyyy-MM-dd}",
        ApplyFormatInEditMode = true)]
    public required DateTime Fecha { get; set; }

    [EnumDataType(typeof(EstadoPedido))]
    public EstadoPedido Estado { get; set; }

    [EnumDataType(typeof(MetodoPago))]
    public MetodoPago MetodoPago { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "El pedido debe contener al menos un producto.")]
    public List<PedidoDetalleRequest> Detalles { get; set; } = new();
}
