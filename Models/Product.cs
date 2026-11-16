using AgroManager.Enums;
namespace AgroManager.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; } 
    public string? Description { get; set; }
    public  decimal Price { get; set; }
    public  UnidadMedida Unit { get; set; }
    public int  CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public  int?  MarcaId { get; set; }
    public Marca? Marca { get; set; }

    public ICollection<LotePallet> LotePallets { get; set; } = new List<LotePallet>();
    public ICollection<PedidoDetalle> PedidoDetalles {get; set;} = new List<PedidoDetalle>();
    public ICollection<MovimientoInventario> MovimientoInventarios {get; set;} = new List<MovimientoInventario>();
    
    public ICollection<EntregaProgramada> EntregasProgramadas {get; set;} = new List<EntregaProgramada>();
}