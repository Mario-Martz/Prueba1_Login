using Prueba1_Login.Domain.Enums;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Infrastructure.Security;
using System;

namespace Prueba1_Login.AppCore.Services
{
    public static class LoginService
    {
        private static IUsuarioRepository _usuarioRepository;

        // Inyección del repositorio
        public static void Initialize(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public static (EstadoLogin estado, string? perfil) ValidarCredenciales(string codigoUsuario, string password)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("🔐 Validando credenciales con HASH + SALT");
            Console.WriteLine($"Usuario ingresado: {codigoUsuario}");
            Console.WriteLine("======================================");

            codigoUsuario = codigoUsuario?.Trim() ?? string.Empty;
            password = password?.Trim() ?? string.Empty;

            // 1️⃣ Validar campos vacíos
            if (string.IsNullOrWhiteSpace(codigoUsuario) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ Error: Usuario o contraseña vacíos");
                return (EstadoLogin.Vacios, null);
            }

            // 2️⃣ Obtener usuario desde la BD
            var usuario = _usuarioRepository.ObtenerPorCodigo(codigoUsuario);

            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario NO existe en la BD");
                return (EstadoLogin.UsuarioIncorrecto, null);
            }

            Console.WriteLine($"✔ Usuario encontrado: {usuario.Codigo}");

            // 3️⃣ Validar hash + salt
            bool valido = SecurityHelper.VerificarPassword(
                password,
                usuario.PasswordHash,
                usuario.PasswordSalt
            );

            if (!valido)
            {
                Console.WriteLine("❌ Contraseña incorrecta (HASH no coincide)");
                return (EstadoLogin.ContraseñaIncorrecta, null);
            }

            Console.WriteLine("✔ Login exitoso con HASH + SALT");
            return (EstadoLogin.Exitoso, usuario.Perfil);
        }
    }
}