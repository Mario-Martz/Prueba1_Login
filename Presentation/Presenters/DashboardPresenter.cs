using Prueba1_Login.AppCore.Session;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Presentation.Interfaces;
using System;
using System.Data;
using System.Windows.Forms;

namespace Prueba1_Login.Presentation.Presenters
{
    public class DashboardPresenter
    {
        private readonly IDashboardView view;

        public DashboardPresenter(IDashboardView view)
        {
            this.view = view;
        }

        public void EjecutarSalida()
        {
            // Preguntar al usuario
            DialogResult respuesta = view.MostrarConfirmacion(
                "¿Seguro que deseas salir de la aplicación?",
                "Confirmar salida"
            );

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                // 1. Limpiar sesión
                SessionManager.CerrarSesion();

                // 2. Cerrar conexión de BD si existe
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }

                // 3. Cerrar aplicación
                view.CerrarAplicacion();
            }
            catch (Exception ex)
            {
                view.MostrarMensaje("Ocurrió un error al cerrar:\n" + ex.Message, "Error");
            }
        }
    }
}