using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public static string HashPassword(this string password)
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = ComputeHash(password, salt);

        var builder = new StringBuilder();
        builder.Append(Convert.ToBase64String(salt));
        builder.Append(":");
        builder.Append(Convert.ToBase64String(hash));
        return builder.ToString();
    }

    public static bool VerifyPassword(this string password, string hashedPassword)
    {
        var parts = hashedPassword.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);

        byte[] computedHash = ComputeHash(password, salt);

        if (computedHash.Length != storedHash.Length)
        {
            return false;
        }
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != storedHash[i])
            {
                return false;
            }
        }
        return true;
    }

    private static byte[] ComputeHash(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(HashSize);
        }
    }
}