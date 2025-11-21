using Prueba1_Login.Debug;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Resources.Fonts_Personalizados;
using System;
using System.Windows.Forms;

namespace Prueba1_Login
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 🧩 Inicializar fuentes personalizadas
            FontManager.Initialize();

            // 🗄️ Verificar conexión antes de iniciar
            if (!DatabaseConnection.ProbarConexion())
            {
                DebugOverlay.Show("❌ Error: No se pudo conectar a la base de datos.");
                MessageBox.Show(
                    "❌ No se pudo conectar con la base de datos.\nVerifica la configuración de Oracle.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // ▶️ Iniciar la aplicación normalmente
            Application.Run(new Login());
        }
    }
}