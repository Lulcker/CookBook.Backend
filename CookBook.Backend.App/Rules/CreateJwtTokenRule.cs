using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Backend.App.Helpers;
using CookBook.Backend.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Backend.App.Rules;

/// <summary>
/// Правило создания Jwt токена
/// </summary>
public class CreateJwtTokenRule
{
    public string Execute(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimsIdentity.DefaultNameClaimType, user.Login),
            new (ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            new (ClaimTypes.Sid, user.Id.ToString())
        };

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: AuthHelper.Issuer,
            audience: AuthHelper.Audience,
            notBefore: now,
            claims: claims,
            expires: now.AddDays(30),
            signingCredentials: new SigningCredentials(AuthHelper.GetSymmetricSecurityKey(), 
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}