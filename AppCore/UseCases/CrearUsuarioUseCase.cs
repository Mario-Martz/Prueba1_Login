using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Infrastructure.Security;
using System;

namespace Prueba1_Login.AppCore.UseCases
{
    public class CrearUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;

        public CrearUsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crear usuario con HASH + SALT sin romper registros existentes.
        /// </summary>
        /// <param name="usuario">Usuario SIN contraseña</param>
        /// <param name="passwordPlano">Contraseña ingresada</param>
        public bool Ejecutar(Usuario usuario, string passwordPlano)
        {
            // 🔹 Validaciones estrictas
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.Codigo))
                throw new Exception("El código de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Perfil))
                throw new Exception("El perfil del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(passwordPlano))
                throw new Exception("La contraseña no puede estar vacía.");

            // 🔹 Verificar si ya existe
            var existente = _repository.ObtenerPorCodigo(usuario.Codigo);

            if (existente != null)
                throw new Exception($"El usuario '{usuario.Codigo}' ya existe en el sistema.");

            // 🔹 Generar HASH + SALT
            var (hash, salt) = SecurityHelper.CrearPasswordHash(passwordPlano);

            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;

            // 🔹 Guardar en el repositorio
            return _repository.Crear(usuario);
        }
    }
}
