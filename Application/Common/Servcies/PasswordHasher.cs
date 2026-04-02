using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;

    public static string HashPassword(this string password)
    {
        // Generate a random salt
        byte[] salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // Compute the hash
        byte[] hash = ComputeHash(password, salt);

        // Combine the salt and hash into a single string
        var builder = new StringBuilder();
        builder.Append(Convert.ToBase64String(salt));
        builder.Append(":");
        builder.Append(Convert.ToBase64String(hash));
        return builder.ToString();
    }

    public static bool VerifyPassword(this string password, string hashedPassword)
    {
        // Extract the salt and hash from the string
        var parts = hashedPassword.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);

        // Compute the hash of the password using the same salt
        byte[] computedHash = ComputeHash(password, salt);

        // Compare the hashes
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