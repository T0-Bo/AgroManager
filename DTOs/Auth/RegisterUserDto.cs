using System.ComponentModel.DataAnnotations;

namespace AgroManager.DTOs.Auth;

public class RegisterUserDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public required string Password { get; set; }
}