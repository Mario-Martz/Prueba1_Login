using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Infrastructure.Security;

namespace Prueba1_Login.AppCore.UseCases
{
    public class ActualizarUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;

        public ActualizarUsuarioUseCase(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public bool Ejecutar(
            Usuario usuarioActualizado,
            string? passwordAnterior = null,
            string? passwordNueva = null,
            string? passwordNuevaRepetida = null)
        {
            if (usuarioActualizado == null)
                throw new ArgumentNullException(nameof(usuarioActualizado));

            if (string.IsNullOrWhiteSpace(usuarioActualizado.Codigo))
                throw new Exception("El código de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuarioActualizado.Nombre))
                throw new Exception("El nombre del usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuarioActualizado.Perfil))
                throw new Exception("El perfil del usuario es obligatorio.");

            // 1) Obtener usuario existente
            var existente = _repository.ObtenerPorCodigo(usuarioActualizado.Codigo);

            if (existente == null)
                throw new Exception($"El usuario '{usuarioActualizado.Codigo}' no existe.");

            // 2) No cambia contraseña si NO mandó campos
            bool quiereCambiarPass =
                !string.IsNullOrWhiteSpace(passwordAnterior) ||
                !string.IsNullOrWhiteSpace(passwordNueva) ||
                !string.IsNullOrWhiteSpace(passwordNuevaRepetida);

            if (!quiereCambiarPass)
            {
                usuarioActualizado.PasswordHash = existente.PasswordHash;
                usuarioActualizado.PasswordSalt = existente.PasswordSalt;
                return _repository.Actualizar(usuarioActualizado);
            }

            // 3) Validar contraseñas
            if (!SecurityHelper.VerificarPassword(passwordAnterior, existente.PasswordHash, existente.PasswordSalt))
                throw new Exception("La contraseña anterior es incorrecta.");

            if (passwordNueva != passwordNuevaRepetida)
                throw new Exception("Las nuevas contraseñas no coinciden.");

            // 4) Crear nuevo hash+salt
            var (hashNuevo, saltNuevo) = SecurityHelper.CrearPasswordHash(passwordNueva);
            usuarioActualizado.PasswordHash = hashNuevo;
            usuarioActualizado.PasswordSalt = saltNuevo;

            // 5) Guardar
            return _repository.Actualizar(usuarioActualizado);
        }
    }
}