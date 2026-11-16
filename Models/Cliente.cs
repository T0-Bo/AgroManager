namespace AgroManager.Models;
public class Cliente
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Documento { get; set; }
    public required string Telefono { get; set; }
    public string? Email { get; set; }       // opcional
    public required string Direccion { get; set; }
    public bool Activo { get; set; }

    public ICollection<Pedido> Pedidos {get; set;} = new List<Pedido>();
}   