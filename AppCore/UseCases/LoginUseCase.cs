using Prueba1_Login.Domain.Enums;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Infrastructure.Security;
using System;

namespace Prueba1_Login.AppCore.UseCases
{
    public class LoginUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginUseCase(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public (EstadoLogin estado, string? perfil) Ejecutar(string codigoUsuario, string password)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("🔐 Validando credenciales con HASH + SALT");
            Console.WriteLine($"Usuario ingresado: {codigoUsuario}");
            Console.WriteLine("======================================");

            // NORMALIZAMOS → elimina espacios + pone mayúsculas
            codigoUsuario = (codigoUsuario?.Trim().ToUpper()) ?? string.Empty;
            password = password?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(codigoUsuario) || string.IsNullOrWhiteSpace(password))
            {
                return (EstadoLogin.Vacios, null);
            }

            // Buscar usuario por código (con Oracle case-insensitive)
            var usuario = _usuarioRepository.ObtenerPorCodigo(codigoUsuario);

            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario NO existe en la BD");
                return (EstadoLogin.UsuarioIncorrecto, null);
            }

            Console.WriteLine($"✔ Usuario encontrado: {usuario.Codigo}");

            // Validar password
            bool valido = SecurityHelper.VerificarPassword(
                password,
                usuario.PasswordHash,
                usuario.PasswordSalt
            );

            if (!valido)
            {
                Console.WriteLine("❌ Contraseña incorrecta");
                return (EstadoLogin.ContraseñaIncorrecta, null);
            }

            Console.WriteLine("✔ Login exitoso");
            return (EstadoLogin.Exitoso, usuario.Perfil);
        }
    }
}