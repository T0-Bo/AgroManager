namespace AgroManager.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string PasswordHash { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<MovimientoInventario> MovimientoInventarios
        { get; set; } = new List<MovimientoInventario>();
}