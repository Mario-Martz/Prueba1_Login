using Prueba1_Login.Domain.Entities;

namespace Prueba1_Login.AppCore.Session
{
    public static class SessionManager
    {
        public static string? UsuarioCodigo { get; private set; }
        public static Usuario? UsuarioActual { get; private set; }
        public static string? Perfil { get; private set; }  // STRING desde BD

        public static void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
            UsuarioCodigo = usuario.Codigo;
            Perfil = usuario.Perfil; // SISTEMAS / USUARIOS / INVITADO
        }

        public static void CerrarSesion()
        {
            UsuarioCodigo = null;
            UsuarioActual = null;
            Perfil = null;
        }
    }
}
