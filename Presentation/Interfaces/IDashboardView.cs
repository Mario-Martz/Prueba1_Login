namespace Prueba1_Login.Presentation.Interfaces
{
    public interface IDashboardView
    {
        void CerrarAplicacion();
        void MostrarMensaje(string mensaje, string titulo);
        DialogResult MostrarConfirmacion(string mensaje, string titulo);
    }
}

