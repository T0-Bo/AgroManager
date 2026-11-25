
using AgroManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AgroManager.Middleware;

public class EnsureActiveUserMiddleware
{
    private readonly RequestDelegate _next;

    public EnsureActiveUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext dbContext)
    {
        // Si no hay un usuario autenticado,
        // dejamos que el pipeline continúe.
        if (context.User.Identity?.IsAuthenticated != true)
        {
            await _next(context);
            return;
        }

        // Buscamos el ID del usuario dentro de los claims.
        var userIdClaim = context.User.FindFirst(
            ClaimTypes.NameIdentifier
        );

        // Si no existe el claim o no contiene un ID válido,
        // rechazamos la petición.
        if (userIdClaim is null ||
            !int.TryParse(userIdClaim.Value, out int userId))
        {
            context.Response.StatusCode =
                StatusCodes.Status401Unauthorized;

            return;
        }

        // Comprobamos en SQLite que el usuario exista
        // y que su cuenta siga activa.
        bool isActive = await dbContext.Users
            .AnyAsync(u => u.Id == userId && u.Activo);

        if (!isActive)
        {
            context.Response.StatusCode =
                StatusCodes.Status401Unauthorized;

            await context.Response.WriteAsync(
                "La cuenta está inactiva o no existe."
            );

            return;
        }

        // El usuario existe y está activo.
        // Permitimos que la petición continúe.
        await _next(context);
    }
}
