using Microsoft.AspNetCore.Identity;

namespace RaveAppAPI.Services.Helpers
{
    public static class Hasher
    {
        public static string HashPassword(string user, string password)
        {
            var hasher = new PasswordHasher<string>();
            return hasher.HashPassword(user, password);
        }
        public static bool VerifyHashedPassword(string user, string hashedPassword, string providedPassword)
        {
            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return !(result == PasswordVerificationResult.Failed);
        }

    }
}
