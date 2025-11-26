using DotNetEnv;
using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Windows.Forms;

namespace Prueba1_Login.Infrastructure.Data
{
    public static class DatabaseConnection
    {
        private static readonly string? connectionString;

        // ==============================
        //   Constructor estático
        // ==============================
        static DatabaseConnection()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string envPath = Path.Combine(baseDir, ".env");

                // 🔹 En entorno de desarrollo el .env suele estar más arriba
                if (!File.Exists(envPath))
                {
                    string altPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.env");
                    if (File.Exists(altPath))
                        envPath = Path.GetFullPath(altPath);
                }

                if (!File.Exists(envPath))
                    throw new FileNotFoundException($"No se encontró el archivo .env en: {envPath}");

                // Cargar variables del archivo .env
                Env.Load(envPath);

                string user = Env.GetString("DB_USER");
                string pass = Env.GetString("DB_PASS");
                string host = Env.GetString("DB_HOST");
                string port = Env.GetString("DB_PORT");
                string service = Env.GetString("DB_SERVICE");

                // Validar variables requeridas
                if (string.IsNullOrWhiteSpace(user) ||
                    string.IsNullOrWhiteSpace(pass) ||
                    string.IsNullOrWhiteSpace(host) ||
                    string.IsNullOrWhiteSpace(port) ||
                    string.IsNullOrWhiteSpace(service))
                {
                    throw new Exception("Faltan variables requeridas en el archivo .env");
                }

                // Construir cadena de conexión
                connectionString =
                    $"User Id={user};Password={pass};Data Source=" +
                    $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))" +
                    $"(CONNECT_DATA=(SERVICE_NAME={service})));";

                Console.WriteLine("✔ Archivo .env cargado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al cargar configuración de BD: {ex.Message}");
                throw;
            }
        }

        // ==============================
        //   Obtener conexión Oracle
        // ==============================
        public static OracleConnection GetConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("No se configuró correctamente la cadena de conexión.");

            return new OracleConnection(connectionString);
        }

        // ==============================
        //   Probar conexión a Oracle
        // ==============================
        public static bool ProbarConexion()
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();

                Console.WriteLine("✔ Conexión a Oracle establecida correctamente.");

                MessageBox.Show(
                    "✔ Conexión a Oracle establecida correctamente.",
                    "Conexión a BD",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al conectar a Oracle: {ex.Message}");

                MessageBox.Show(
                    $"❌ Error al conectar a Oracle:\n\n{ex.Message}",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }
        }
    }
}
