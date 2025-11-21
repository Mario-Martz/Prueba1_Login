using Prueba1_Login.Domain.Entities;

namespace Prueba1_Login.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        // Obtener un usuario por su código (CVE_USUARIO)
        Usuario? ObtenerPorCodigo(string codigo);

        // Obtener un usuario por nombre completo o parcial (OPCIONAL)
        Usuario? ObtenerPorNombre(string nombre);

        // Obtener todos los usuarios (lista de administración)
        List<Usuario> ObtenerTodos();

        // Eliminar por clave primaria
        bool Eliminar(string codigo);

        // Crear usuario nuevo
        bool Crear(Usuario usuario);

        // Actualizar usuario existente
        bool Actualizar(Usuario usuario);
    }
}
