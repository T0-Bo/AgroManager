using System.ComponentModel.DataAnnotations;
using AgroManager.Enums;

namespace AgroManager.DTOs;

public class PedidoRequest
{
    [Range(1, int.MaxValue)]
    public  int ClienteId { get; set; }
    
    [Required(ErrorMessage = "La fecha es obligatoria.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public required DateTime Fecha {get; set;}

    [EnumDataType(typeof(EstadoPedido), ErrorMessage = "Unit must be a valid EstadoPedido.")]
    public  EstadoPedido Estado {get; set;}

    [EnumDataType(typeof(MetodoPago), ErrorMessage = "Unit must be a valid MetodoPago.")]
    public  MetodoPago MetodoPago {get; set;}

}