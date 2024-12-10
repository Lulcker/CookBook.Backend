using System.Net.Mime;
using System.Text;
using CookBook.Backend.App.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CookBook.Backend;

public class GlobalExceptionHandler
{
    public static async Task Handle(HttpContext context)
    {
        var exception =
            context.Features.Get<IExceptionHandlerPathFeature>()!.Error;
        
        var logger = context.RequestServices.GetService<ILogger<GlobalExceptionHandler>>()!;

        if (exception is AccessDeniedException) // доступ запрещён
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.WritePlainToResponse(exception.Message);
        }
        else if (exception is BusinessException) // бизнес-исключение
        {
            logger.LogInformation("Business exception: {Message}", exception.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.WritePlainToResponse(exception.Message);
        }
        else // неожиданное исключение
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            await context.WritePlainToResponse("Произошла внутренняя ошибка системы");

            logger.LogError(exception, "Произошла необработанная ошибка: {error}", exception.Message);
        }
    }
}

internal static class GlobalExceptionHandlerExtensions
{
    internal static async Task WritePlainToResponse(this HttpContext context, string message)
    {
        context.Response.ContentType = $"{MediaTypeNames.Text.Plain}; charset=utf-8";

        await context.Response.WriteAsync(message, Encoding.UTF8);
    }
}