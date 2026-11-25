using AgroManager.DTOs.Auth;
using AgroManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserService userService,
        JwtTokenService jwtTokenService,
        IConfiguration configuration)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _configuration = configuration;
    }

    
    [HttpPost("initialize")]
    public async Task<IActionResult> Initialize(
        [FromBody] RegisterUserDto dto)
    {
        string? expectedSecret = _configuration["AdminSetup:Secret"];

        if (string.IsNullOrWhiteSpace(expectedSecret))
            return StatusCode(500, "El secreto de inicialización no está configurado.");

        if (!Request.Headers.TryGetValue("X-Admin-Setup", out var providedSecret))
            return Unauthorized("Se requiere el secreto de inicialización.");

        if (!System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(
            System.Text.Encoding.UTF8.GetBytes(expectedSecret),
            System.Text.Encoding.UTF8.GetBytes(providedSecret.ToString())))
        {
            return Unauthorized("Secreto de inicialización inválido.");
        }

        var admin = await _userService.InitializeAdminAsync(
            dto.Name,
            dto.Password
        );

        if (admin is null)
            return Conflict("El sistema ya fue inicializado.");

        string token = _jwtTokenService.GenerateToken(admin);

        return Ok(new
        {
            message = "Administrador inicial creado correctamente.",
            token,
            user = new
            {
                admin.Id,
                admin.Name,
                admin.Role
            }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginDto dto)
    {
        var user = await _userService.AuthenticateAsync(
            dto.Name,
            dto.Password
        );

        if (user is null)
            return Unauthorized("Usuario o contraseña incorrectos.");

        string token = _jwtTokenService.GenerateToken(user);

        return Ok(new
        {
            message = "Inicio de sesión exitoso.",
            token,
            user = new
            {
                user.Id,
                user.Name,
                user.Role
            }
        });
    }

}