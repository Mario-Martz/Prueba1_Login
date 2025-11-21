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
        /// Crear usuario con hashing de contraseña
        /// </summary>
        /// <param name="usuario">Objeto usuario SIN contraseña</param>
        /// <param name="passwordPlano">Contraseña ingresada en pantalla</param>
        public bool Ejecutar(Usuario usuario, string passwordPlano)
        {
            if (usuario == null) return false;
            if (string.IsNullOrWhiteSpace(usuario.Codigo)) return false;
            if (string.IsNullOrWhiteSpace(usuario.Nombre)) return false;
            if (string.IsNullOrWhiteSpace(passwordPlano)) return false;
            if (string.IsNullOrWhiteSpace(usuario.Perfil)) return false;

            // Validar que no exista
            var existente = _repository.ObtenerPorCodigo(usuario.Codigo);
            if (existente != null)
                throw new Exception($"El usuario '{usuario.Codigo}' ya existe.");

            // Generar hash + salt
            var (hash, salt) = SecurityHelper.CrearPasswordHash(passwordPlano);

            usuario.PasswordHash = hash;
            usuario.PasswordSalt = salt;

            return _repository.Crear(usuario);
        }
    }
}
