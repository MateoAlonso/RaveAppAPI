using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace RaveAppAPI.Services.Helpers
{
    // De https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
    public static class Hasher
    {
        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: password!,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 100000,
                 numBytesRequested: 256 / 8));
        }

    }
}
