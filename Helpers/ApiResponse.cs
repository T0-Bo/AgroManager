using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AgroManager.Helpers
{
    // Envelope estándar para TODAS las respuestas
    public class ApiResponseEnvelope
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public int StatusCode { get; set; }
    }

    public static class ApiResponse
    {

        // 201 - Recurso creado (con Location header opcional)
        public static IActionResult Created(object data, string? message = null, string? location = null)
            => OkInternal(data, 201, message ?? "Recurso creado", location);

        // 200 - OK
        public static IActionResult Ok(object data, string? message = null)
            => OkInternal(data, 200, message ?? "OK");
        // 400 - Bad Request
        public static IActionResult BadRequest(string message = "Solicitud inválida")
            => ErrorInternal(message, 400);

        // 404 - Not Found
        public static IActionResult NotFound(string message = "Recurso no encontrado")
            => ErrorInternal(message, 404);

        // 409 - Conflict
        public static IActionResult Conflict(string message = "Conflicto de datos")
            => ErrorInternal(message, 409);

        // 500 - Server Error
        public static IActionResult ServerError(string message = "Error interno del servidor")
            => ErrorInternal(message, 500);

        // --- Internos ---
        private static IActionResult OkInternal(object data, int code, string message, string? location = null)
        {
            var envelope = new ApiResponseEnvelope
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = code
            };


            var result = new JsonResult(envelope) { StatusCode = code };

            return location != null
                ? new CreatedResult(location, envelope) { StatusCode = code }
                : result;
        }

        private static IActionResult ErrorInternal(string message, int code)
        {
            var envelope = new ApiResponseEnvelope
            {
                Success = false,
                Message = message,
                Data = null,
                StatusCode = code
            };

            return new JsonResult(envelope) { StatusCode = code };
        }
    }
}   