using System;
using System.Security.Cryptography;

namespace DAL
{

    public class PasswordHasher
    {
        private const int SaltSize = 16;   // Salt size in bytes
        private const int HashSize = 20;   // Hash size in bytes
        private const int Iterations = 100000;  // Number of iterations

        public static string HashPassword(string? password)
        {
            // Generate a random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create the password hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash into a single byte array
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert the combined byte array to a base64-encoded string
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public static bool VerifyPassword(string enteredPassword, string? hashedPassword)
        {
            // Convert the stored hash back to a byte array
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Extract the salt from the stored hash
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Compute the hash of the entered password using the stored salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            bool hashesMatch = hashBytes.Skip(SaltSize).SequenceEqual(hash);

            return hashesMatch;
        }
     
    }

}
