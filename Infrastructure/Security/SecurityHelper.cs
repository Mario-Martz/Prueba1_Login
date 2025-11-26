using System.Security.Cryptography;
using System.Text;

namespace Prueba1_Login.Infrastructure.Security
{
    public static class SecurityHelper
    {
        private const int SaltSize = 32; // 32 bytes = 256 bits

        // ===========================================
        // GENERAR HASH + SALT
        // ===========================================
        public static (string Hash, string Salt) CrearPasswordHash(string password)
        {
            // 1. Generar un SALT con seguridad criptográfica
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            // 2. Generar hash usando HMACSHA256 con el salt
            string hash = GenerarHash(password, saltBytes);

            // 3. Convertir a Base64
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }

        // ===========================================
        // VERIFICAR PASSWORD
        // ===========================================
        public static bool VerificarPassword(string password, string hashGuardado, string saltGuardado)
        {
            // 1. Convertir SALT guardado a bytes
            byte[] saltBytes = Convert.FromBase64String(saltGuardado);

            // 2. Generar hash con el salt guardado
            string hashIntento = GenerarHash(password, saltBytes);

            // 3. Comparar hashes
            return hashIntento == hashGuardado;
        }

        // ===========================================
        // MÉTODO PRIVADO → genera hash con un salt dado
        // ===========================================
        private static string GenerarHash(string password, byte[] saltBytes)
        {
            using (var hmac = new HMACSHA256(saltBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
