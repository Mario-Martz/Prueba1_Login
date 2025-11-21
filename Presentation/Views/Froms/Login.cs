using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.AppCore.Session;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Froms;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Resources.Controls;
using Prueba1_Login.Resources.Fonts_Personalizados;
using System;
using System.Windows.Forms;

namespace Prueba1_Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Inicializa fuentes personalizadas
            FontManager.Initialize();

            // Aplica las fuentes a todos los controles del formulario
            AplicarFuenteAControles(this);
        }

        // Aplica fuentes Montserrat a todos los controles
        private void AplicarFuenteAControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Font = FontManager.GetFont(AppFont.MontserratRegular, 16f);

                else if (ctrl is Button || ctrl is CustomIconButton)
                    ctrl.Font = FontManager.GetFont(AppFont.MontserratBold, 16f);

                else if (ctrl is TextBox txt)
                    txt.Font = FontManager.GetFont(AppFont.MontserratRegular, 16f);

                if (ctrl.HasChildren)
                    AplicarFuenteAControles(ctrl);
            }
        }

        private void btn_inicio_Login_Click(object sender, EventArgs e)
        {
            string nombre = txt_User_Login.Text.Trim();
            string pass = txt_Pass_Login.Text.Trim();

            Console.WriteLine("=== INICIANDO LOGIN POR NOMBRE ===");
            Console.WriteLine("Nombre ingresado: " + nombre);
            Console.WriteLine("Contraseña ingresada: " + pass);

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Debes ingresar nombre y contraseña", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OracleConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    Console.WriteLine("✔ Conexión a Oracle abierta.");

                    // ✅ MODIFICADO: Buscar por NOMBRE en lugar de CVE_USUARIO
                    string query = @"
                        SELECT U.CVE_USUARIO, U.NOMBRE, U.APELLIDO_PATERNO, U.APELLIDO_MATERNO, P.DESCRIPCION
                        FROM USUARIOS U
                        INNER JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL
                        WHERE UPPER(U.NOMBRE) = :nombre AND U.CONTRASENA = :pass";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("nombre", nombre.ToUpper())); // ← MODIFICADO
                        cmd.Parameters.Add(new OracleParameter("pass", pass));

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                Console.WriteLine("❌ Nombre o contraseña incorrectos.");

                                // Debug: mostrar qué nombres existen
                                MostrarNombresDisponibles(conn);

                                MessageBox.Show("Nombre o contraseña incorrectos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            string codigoUsuario = reader.GetString(0).Trim();
                            string nombreCompleto = reader.GetString(1).Trim();
                            string apellidoPaterno = reader.IsDBNull(2) ? "" : reader.GetString(2).Trim();
                            string apellidoMaterno = reader.IsDBNull(3) ? "" : reader.GetString(3).Trim();
                            string perfil = reader.GetString(4).Trim().ToUpper();

                            Console.WriteLine("✔ Login exitoso por NOMBRE:");
                            Console.WriteLine($"   - Código: {codigoUsuario}");
                            Console.WriteLine($"   - Nombre: {nombreCompleto}");
                            Console.WriteLine($"   - Perfil: {perfil}");

                            // ✅ Crear objeto Usuario completo y guardar sesión
                            Usuario usuarioLogin = new Usuario
                            {
                                Codigo = codigoUsuario,
                                Nombre = nombreCompleto,
                                ApellidoPaterno = apellidoPaterno,
                                ApellidoMaterno = apellidoMaterno,
                                Perfil = perfil
                            };

                            SessionManager.IniciarSesion(usuarioLogin);

                            MessageBox.Show(
                                $"Bienvenido {nombreCompleto}\nPerfil: {perfil}",
                                "ACCESO CONCEDIDO",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            // Abrir Dashboard
                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR DE SISTEMA:");
                Console.WriteLine(ex.ToString());
                MessageBox.Show(
                    "Error al intentar iniciar sesión.\n\n" + ex.Message,
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ✅ MÉTODO NUEVO: Para debug - muestra los nombres disponibles
        private void MostrarNombresDisponibles(OracleConnection conn)
        {
            try
            {
                string query = "SELECT NOMBRE, CVE_USUARIO FROM USUARIOS ORDER BY NOMBRE";
                using (OracleCommand cmd = new OracleCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("📋 Nombres disponibles en la BD:");
                    while (reader.Read())
                    {
                        string nombreBD = reader.GetString(0).Trim();
                        string usuarioBD = reader.GetString(1).Trim();
                        Console.WriteLine($"   - '{nombreBD}' (Usuario: {usuarioBD})");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener nombres: {ex.Message}");
            }
        }

        private void pnl_login_Paint(object sender, PaintEventArgs e)
        {
            // vacío
        }
    }
}