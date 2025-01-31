using System;
using System.Security.Cryptography;

namespace HRIS.Infrastructure.Utils
{
    public static class PasswordHasher
    {
        // Konfigurasi
        private const int SaltSize = 16; // Panjang salt dalam byte
        private const int KeySize = 32; // Panjang hash dalam byte
        private const int Iterations = 10000; // Jumlah iterasi hashing
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        // Format hash: {Salt}:{Hash}:{Iterations}
        public static string HashPassword(string password)
        {
            // Generate salt
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Hash password dengan PBKDF2
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithm,
                KeySize
            );

            // Encode salt dan hash ke base64 untuk penyimpanan
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}:{Iterations}";
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Pisahkan salt, hash, dan iterasi
            var parts = hashedPassword.Split(':');
            if (parts.Length != 3)
                throw new FormatException("Invalid hashed password format.");

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var iterations = int.Parse(parts[2]);

            // Hash ulang password menggunakan salt dan iterasi yang sama
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithm,
                hash.Length
            );

            // Bandingkan hash secara aman
            return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
        }
    }
}
