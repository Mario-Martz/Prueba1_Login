using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.AppCore.Session;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Froms;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Infrastructure.Data.Repositories;
using Prueba1_Login.Infrastructure.Security;
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
            string codigoUsuario = txt_User_Login.Text.Trim().ToUpper();   // NORMALIZADO
            string password = txt_Pass_Login.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigoUsuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Debes ingresar usuario y contraseña", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                IUsuarioRepository repo = new UsuarioRepository();
                Usuario? usuario = repo.ObtenerPorCodigo(codigoUsuario);   // Busca por CVE_USUARIO

                if (usuario == null)
                {
                    MessageBox.Show("Usuario no encontrado", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool valido = SecurityHelper.VerificarPassword(
                    password,
                    usuario.PasswordHash,
                    usuario.PasswordSalt
                );

                if (!valido)
                {
                    MessageBox.Show("Contraseña incorrecta", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SessionManager.IniciarSesion(usuario);

                MessageBox.Show(
                    $"Bienvenido {usuario.Nombre}\nPerfil: {usuario.Perfil}",
                    "ACCESO CONCEDIDO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión:\n" + ex.Message,
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pnl_login_Paint(object sender, PaintEventArgs e)
        {
            // vacío
        }

    }
}