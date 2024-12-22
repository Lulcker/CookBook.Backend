using System.Security.Claims;
using CookBook.Backend.App.Contracts;
using CookBook.Backend.Domain.Dictionaries;

namespace CookBook.Backend.Middlewares;

public class UserInfoMiddleware
{
    #region Fields
    
    private readonly RequestDelegate _next;

    #endregion

    #region Constructor

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="next"></param>
    public UserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    #region Methods

    /// <summary>
    /// InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        var userInfoProvider = context.RequestServices.GetRequiredService<IUserInfoProvider>();

        var userName = context.User.Identity?.Name;

        if (userName is null)
        {
            userInfoProvider.Role = UserRole.Anonymous;
            await _next.Invoke(context);
            
            return;
        }
    
        Enum.TryParse<UserRole>(context.User.Claims
            .FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)!.Value, out var userRole);
        
        userInfoProvider.Id = Guid.Parse(context.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Sid)!.Value);
        userInfoProvider.Login = userName;
        userInfoProvider.Role = userRole;

        await _next.Invoke(context);
    }

    #endregion
}