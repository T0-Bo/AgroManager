using AgroManager.Enums;

namespace AgroManager.Models;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public UserRole Role { get; set; } = UserRole.Employee;

    public ICollection<MovimientoInventario> MovimientoInventarios
        { get; set; } = new List<MovimientoInventario>();
}