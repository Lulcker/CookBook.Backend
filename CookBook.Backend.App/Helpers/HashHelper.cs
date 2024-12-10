using System.Security.Cryptography;
using System.Text;

namespace CookBook.Backend.App.Helpers;

public class HashHelper
{
    #region Fields

    private static readonly byte[] pepperBytes = "CarumbAnEgblwSHKW"u8.ToArray();

    #endregion
    
    #region Methods

    /// <summary>
    /// Метод создания соли
    /// </summary>
    /// <returns>Соль</returns>
    public static string GenerateSalt() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    
    /// <summary>
    /// Метод создания хэша пароля
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="salt">Соль</param>
    /// <returns>Хэш пароля</returns>
    public static string GetHashString(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltBytes = Encoding.UTF8.GetBytes(salt);
        var passwordWithSaltAndPepperBytes = new byte[passwordBytes.Length + saltBytes.Length + pepperBytes.Length];

        Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltAndPepperBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, passwordWithSaltAndPepperBytes, passwordBytes.Length, saltBytes.Length);
        Buffer.BlockCopy(pepperBytes, 0, passwordWithSaltAndPepperBytes, passwordBytes.Length + saltBytes.Length, pepperBytes.Length);

        return Convert.ToBase64String(sha256.ComputeHash(passwordWithSaltAndPepperBytes));
    }

    #endregion
}