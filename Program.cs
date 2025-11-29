using Microsoft.VisualBasic;
using Prueba1_Login.Froms;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Infrastructure.Security;
using Prueba1_Login.Resources.Fonts_Personalizados;
using System;
using System.IO; // 👈 NECESARIO PARA TRABAJAR CON ARCHIVOS
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

            // ===========================================
            // 🔐 MODO ADMIN → GENERADOR DE HASH + SALT
            // ===========================================
            //var result = MessageBox.Show(
            //    "¿Deseas abrir el generador de HASH + SALT?\n(Solo administradores)",
            //    "Modo Administrador",
            //    MessageBoxButtons.YesNo,
            //    MessageBoxIcon.Question
            //);

            //if (result == DialogResult.Yes)
            //{
            //    EjecutarGeneradorHash();
            //    return;
            //}

            // 🗄️ Verificar conexión antes de iniciar
            if (!DatabaseConnection.ProbarConexion())
            {
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
            //Application.Run(new Dashboard());

        }

        // ======================================================
        // 🧰 GENERADOR HASH + SALT (CON EXPORTACIÓN A TXT)
        // ======================================================
        private static void EjecutarGeneradorHash()
        {
            // Pedir la contraseña
            string password = Interaction.InputBox(
                "Ingresa la contraseña en texto plano:",
                "Generador HASH + SALT",
                ""
            );

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "La contraseña no puede estar vacía.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Generar HASH + SALT
            var (hash, salt) = SecurityHelper.CrearPasswordHash(password);

            // --- 🚨 NUEVA FUNCIONALIDAD: EXPORTAR A TXT ---

            string nombreArchivo = "HashSalt_Generated.txt";
            string contenidoArchivo =
                $"Contraseña Original: {password}\n" +
                "==========================================\n" +
                $"HASH generado:\n{hash}\n\n" +
                $"SALT generado:\n{salt}\n" +
                "==========================================\n" +
                "Estos valores deben usarse en el INSERT de SQL.";

            try
            {
                File.WriteAllText(nombreArchivo, contenidoArchivo);
                Console.WriteLine($"✔ Archivo guardado: {nombreArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al guardar archivo: {ex.Message}");
                MessageBox.Show($"Error al guardar el archivo TXT: {ex.Message}", "Error de Escritura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // ---------------------------------------------


            // Mostrar resultados en la ventana
            string mensaje =
                "=== HASH GENERADO ===\n\n" +
                $"Contraseña original: {password}\n\n" +
                $"HASH:\n{hash}\n\n" +
                $"SALT:\n{salt}\n\n" +
                "Copia estos valores y colócalos en tu INSERT de SQL.\n\n" +
                $"El HASH y SALT han sido guardados en el archivo: {nombreArchivo}"; // 👈 Mensaje actualizado

            MessageBox.Show(
                mensaje,
                "HASH + SALT Generados",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}