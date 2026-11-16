namespace AgroManager.Models;

public class Proveedor
{
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Documento { get; set; }
    public required string Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }

    public bool Activo { get; set; } = true;

    public ICollection<EntregaProgramada> EntregasProgramadas { get; set; }
        = new List<EntregaProgramada>();
}