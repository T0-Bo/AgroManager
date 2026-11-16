using AgroManager.Models;
namespace AgroManager.DTOs;

public class ClienteResponse
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Documento { get; set; }
    public string Telefono { get; set; }
    public string? Email { get; set; }
    public string Direccion { get; set; }
    public bool Activo { get; set; }

    public ClienteResponse(Cliente cliente)
    {
        Id = cliente.Id;
        Name = cliente.Name;
        Documento = cliente.Documento;
        Telefono = cliente.Telefono;
        Email = cliente.Email;
        Direccion = cliente.Direccion;
        Activo = cliente.Activo;

    }
}   