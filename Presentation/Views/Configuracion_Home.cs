using Prueba1_Login.AppCore.Services;
using Prueba1_Login.AppCore.Session;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Enums;
using Prueba1_Login.Infrastructure.Data.Repositories;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Views
{
    public partial class Configuracion_Home : Form
    {
        // ============================================
        // 🔵 SERVICIOS, REPOS Y VARIABLES GLOBALES
        // ============================================
        private readonly UsuarioService _usuarioService = new UsuarioService(new UsuarioRepository());

        public Configuracion_Home(int tabIndex = 0)
        {
            InitializeComponent();
            FontManager.Initialize();
            AplicarFuenteAControles(this);

            tabConfiguraciones.Appearance = TabAppearance.FlatButtons;
            tabConfiguraciones.ItemSize = new Size(0, 1);
            tabConfiguraciones.SizeMode = TabSizeMode.Fixed;

            // Eventos principales
            this.Load += Configuracion_Home_Load;
            tabConfiguraciones.SelectedIndexChanged += tabConfiguraciones_SelectedIndexChanged;
            userTable.OnDelete += UserTable_OnDelete;
            userTable.OnEdit += UserTable_OnEdit;

            // Cargar perfiles
            CargarPerfilesUsuarios();

            tabConfiguraciones.SelectedIndex = tabIndex;
        }

        // ============================================
        // 🔵 MÉTODOS GENERALES DEL FORM
        // ============================================
        private void AplicarFuenteAControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Font = FontManager.GetFont(AppFont.MontserratRegular, 22f);
                else if (ctrl is Button btn)
                    btn.Font = FontManager.GetFont(AppFont.MontserratBold, 26f);
                else if (ctrl is TextBox txt)
                    txt.Font = FontManager.GetFont(AppFont.MontserratRegular, 18f);

                if (ctrl.HasChildren)
                    AplicarFuenteAControles(ctrl);
            }
        }

        // ============================================
        // 🔴 TAB 1 — ADMINISTRACIÓN DE USUARIOS
        // ============================================
        private void Configuracion_Home_Load(object? sender, EventArgs e)
        {
            string perfil = SessionManager.Perfil ?? "";

            if (perfil == "SISTEMAS" &&
                tabConfiguraciones.SelectedTab == tab_PanelAdministracionU)
            {
                userTable.CargarDatos();
            }
            else
            {
                userTable.Enabled = false;
            }
        }

        private void tabConfiguraciones_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string perfil = SessionManager.Perfil ?? "";

            if (perfil != "SISTEMAS")
                return;

            if (tabConfiguraciones.SelectedTab == tab_PanelAdministracionU)
            {
                userTable.CargarDatos();
            }
        }

        private void UserTable_OnEdit(Usuario usuario)
        {
            MessageBox.Show($"Editar usuario: {usuario.Nombre}", "Edición", MessageBoxButtons.OK);
            tabConfiguraciones.SelectedTab = tab_ModificarU;
        }

        private void UserTable_OnDelete(Usuario usuario)
        {
            string perfil = SessionManager.Perfil ?? "";

            if (perfil != "SISTEMAS")
            {
                MessageBox.Show("No tienes permisos para eliminar usuarios.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show(
                $"¿Seguro que deseas eliminar al usuario '{usuario.Nombre}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dr != DialogResult.Yes) return;

            try
            {
                var repo = new UsuarioRepository();
                repo.Eliminar(usuario.Codigo);

                MessageBox.Show("Usuario eliminado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                userTable.CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error eliminando usuario: " + ex.Message);
            }
        }

        private void btn_Crear_Click_1(object sender, EventArgs e)
        {
            tabConfiguraciones.SelectedTab = tab_CrearUusuario;
        }

        // ============================================
        // 🟢 TAB 2 — CREAR USUARIO (USANDO HASH)
        // ============================================
        private void add_user_CreateU_Click(object sender, EventArgs e)
        {
            try
            {
                // ---- VALIDACIONES ----
                if (string.IsNullOrWhiteSpace(txtCre_Nombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCre__Apell_P.Text) ||
                    string.IsNullOrWhiteSpace(txtCre__Apell_M.Text) ||
                    string.IsNullOrWhiteSpace(txtCre_password.PasswordValue) ||
                    string.IsNullOrWhiteSpace(txtCre_Repit_password.PasswordValue))
                {
                    MessageBox.Show("Por favor completa todos los campos.",
                        "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtCre_password.PasswordValue != txtCre_Repit_password.PasswordValue)
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Perfil
                if (comboBox_Pefl_Users.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un perfil para el usuario.",
                        "Perfil requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PerfilUsuario perfilEnum = (PerfilUsuario)comboBox_Pefl_Users.SelectedItem;
                string perfilTextoBD = perfilEnum.ToString().ToUpper();

                string codigoGenerado = GenerarCodigoUsuario();

                // ⚡ Crear usuario SIN HASH (el UseCase lo hace)
                Usuario nuevo = new Usuario
                {
                    Codigo = codigoGenerado,
                    Nombre = txtCre_Nombre.Text.Trim(),
                    ApellidoPaterno = txtCre__Apell_P.Text.Trim(),
                    ApellidoMaterno = txtCre__Apell_M.Text.Trim(),
                    Perfil = perfilTextoBD
                };

                // ⚡ UseCase genera HASH y SALT internamente
                bool creado = _usuarioService.CrearUsuario(nuevo, txtCre_password.PasswordValue.Trim());

                if (!creado)
                {
                    MessageBox.Show("No se pudo crear el usuario. Verifica si ya existe.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Usuario creado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCamposCrearUsuario();
                userTable.CargarDatos();
                tabConfiguraciones.SelectedTab = tab_PanelAdministracionU;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================
        // 🟣 TAB 3 — EDITAR USUARIO (AÚN PENDIENTE)
        // ============================================
        // Aquí se implementarán:
        // - Cargar usuario seleccionado
        // - Validar contraseña actual
        // - Guardar edición
        // ============================================



        // ============================================
        // ⚙️ MÉTODOS PRIVADOS
        // ============================================
        private void CargarPerfilesUsuarios()
        {
            comboBox_Pefl_Users.DataSource = Enum.GetValues(typeof(PerfilUsuario));
        }

        private string GenerarCodigoUsuario()
        {
            return "USR_" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        private void LimpiarCamposCrearUsuario()
        {
            txtCre_Nombre.Text = "";
            txtCre__Apell_P.Text = "";
            txtCre__Apell_M.Text = "";
            txtCre_password.Text = "";
            txtCre_Repit_password.Text = "";
            comboBox_Pefl_Users.SelectedIndex = -1;
        }
    }
}
