
using AgroManager.DTOs.Users;
using AgroManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(
        [FromBody] CreateEmployeeDto dto)
    {
        var user = await _userService.CreateEmployeeAsync(
            dto.Name,
            dto.Password
        );

        if (user is null)
            return Conflict("Ya existe un usuario con ese nombre.");

        var response = new
        {
            user.Id,
            user.Name,
            user.Role,
            user.Activo
        };

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = user.Id },
            response
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();

        var result = users.Select(user => new
        {
            user.Id,
            user.Name,
            user.Role,
            user.Activo
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
            return NotFound("El usuario no existe.");

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Role,
            user.Activo
        });
    }

    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        bool deactivated =
            await _userService.DeactivateEmployeeAsync(id);

        if (!deactivated)
            return NotFound("El usuario no existe o no es un empleado.");

        return Ok(new
        {
            message = "Empleado desactivado correctamente."
        });
    }
}
