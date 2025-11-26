using Prueba1_Login.AppCore.UseCases;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Enums;
using Prueba1_Login.Domain.Interfaces;

namespace Prueba1_Login.AppCore.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly CrearUsuarioUseCase _crearUsuario;
        private readonly LoginUseCase _login;

        // ================================
        // ÚNICO CONSTRUCTOR CORRECTO
        // ================================
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
            _crearUsuario = new CrearUsuarioUseCase(repo);
            _login = new LoginUseCase(repo);
        }


        public (EstadoLogin estado, string? perfil) Login(string codigo, string password)
        {
            return _login.Ejecutar(codigo, password);
        }

        // ================================
        // OBTENER POR CÓDIGO
        // ================================
        public Usuario? ObtenerPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return null;
            return _repo.ObtenerPorCodigo(codigo);
        }

        // ================================
        // OBTENER TODOS
        // ================================
        public List<Usuario> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        // ================================
        // NUEVO: OBTENER POR NOMBRE
        // ================================
        public Usuario? ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;
            return _repo.ObtenerPorNombre(nombre);
        }

        // ================================
        // CREAR USUARIO
        // ================================
        public bool CrearUsuario(Usuario usuario, string passwordPlano)
        {
            return _crearUsuario.Ejecutar(usuario, passwordPlano);
        }

        // ================================
        // ACTUALIZAR USUARIO
        // ================================
        public bool Actualizar(Usuario usuario)
        {
            return _repo.Actualizar(usuario);
        }

        // ================================
        // ELIMINAR USUARIO
        // ================================
        public bool Eliminar(string codigo)
        {
            return _repo.Eliminar(codigo);
        }
    }
}
