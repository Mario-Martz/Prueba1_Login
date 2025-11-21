using Prueba1_Login.Domain.Entities;

namespace Prueba1_Login.Presentation.Interfaces
{
    public interface IUsuarioView
    {
        // Mostrar un usuario individual
        void MostrarUsuario(Usuario usuario);

        // Mostrar lista completa
        void MostrarLista(List<Usuario> lista);

        // Mensajes generales
        void MostrarMensaje(string texto);

        // Mensajes de error
        void MostrarError(string texto);

        // Limpiar los campos de entrada
        void LimpiarCampos();

        // Bloquear/Habilitar controles (para roles)
        void HabilitarControles(bool habilitar);

        // Accesores para campos del formulario
        string CodigoInput { get; }
        string NombreInput { get; }
        string ApellidoPaternoInput { get; }
        string ApellidoMaternoInput { get; }
        string ContrasenaInput { get; }
        string PerfilInput { get; }

        // Eventos que la Vista expone al Presenter
        event EventHandler BuscarClicked;
        event EventHandler CrearClicked;
        event EventHandler ActualizarClicked;
        event EventHandler EliminarClicked;
        event EventHandler CargarListaClicked;
    }
}
