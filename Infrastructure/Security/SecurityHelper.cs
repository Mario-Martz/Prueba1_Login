using System.Security.Cryptography;
using System.Text;

namespace Prueba1_Login.Infrastructure.Security
{
    public static class SecurityHelper
    {
        public static (string Hash, string Salt) CrearPasswordHash(string password)
        {
            using var hmac = new HMACSHA256();
            var saltBytes = hmac.Key;
            var salt = Convert.ToBase64String(saltBytes);

            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToBase64String(hashBytes);

            return (hash, salt);
        }

        public static bool VerificarPassword(string password, string hashGuardado, string saltGuardado)
        {
            var saltBytes = Convert.FromBase64String(saltGuardado);
            using var hmac = new HMACSHA256(saltBytes);

            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashIntento = Convert.ToBase64String(hashBytes);

            return hashIntento == hashGuardado;
        }
    }
}