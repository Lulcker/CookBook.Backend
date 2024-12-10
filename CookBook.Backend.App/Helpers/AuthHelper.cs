using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Backend.App.Helpers;

public class AuthHelper
{
    public const string Issuer = "CookBookServer";
    
    public const string Audience = "CookBookClient";
    
    private const string Key = "CookBook_dd4d*038(43dikc(dnmde$g{fvdov+7f9";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}