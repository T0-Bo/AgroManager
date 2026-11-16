namespace AgroManager.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string PasswordHash { get; set; }

    public ICollection<MovimientoInventario> MovimientoInventarios
        { get; set; } = new List<MovimientoInventario>();
}