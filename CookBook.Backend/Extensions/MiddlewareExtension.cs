using CookBook.Backend.Middlewares;

namespace CookBook.Backend.Extensions;

public static class MiddlewareExtension
{
    #region Middlewares

    /// <summary>
    /// Middleware для сбора информации о пользователе
    /// </summary>
    /// <param name="builder"></param>
    public static void UseUserInfo(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<UserInfoMiddleware>();
    }

    #endregion
}