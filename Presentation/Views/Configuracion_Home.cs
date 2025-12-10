using Prueba1_Login.AppCore.Services;
using Prueba1_Login.AppCore.Session;
using Prueba1_Login.AppCore.UseCases;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Enums;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.Infrastructure.Data.Repositories;
using Prueba1_Login.Infrastructure.Security;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Views
{
    public partial class Configuracion_Home : Form
    {
        // ============================================
        // 🔵 SERVICIOS, REPOS Y VARIABLES GLOBALES
        // ============================================

        private readonly UsuarioService _usuarioService = new UsuarioService(new UsuarioRepository());

        // Repo + UseCase nuevos ⬇⬇⬇
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ActualizarUsuarioUseCase _actualizarUsuarioUseCase;

        // 🔵 Usuario actualmente en edición
        private Usuario? _usuarioEnEdicion;


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

            // ================================
            // 🔥 CREAR REPOSITORY + USE CASE
            // ================================
            _usuarioRepository = new UsuarioRepository();
            _actualizarUsuarioUseCase = new ActualizarUsuarioUseCase(_usuarioRepository);

            // Cargar perfiles
            CargarPerfilesUsuarios();
            CargarPerfilesModificacion();

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
            _usuarioEnEdicion = usuario;

            TextBoxMod_Nombre.Text = usuario.Nombre;
            TextBoxMod_Apell_P.Text = usuario.ApellidoPaterno;
            TextBoxMod_Apell_M.Text = usuario.ApellidoMaterno;

            if (Enum.TryParse<PerfilUsuario>(usuario.Perfil, true, out var perfilEnum))
                comboBox_Mod_Perfil.SelectedItem = perfilEnum;

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
        // 🟢 TAB 2 — CREAR USUARIO
        // ============================================
        private void add_user_CreateU_Click(object sender, EventArgs e)
        {
            try
            {
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

                if (comboBox_Pefl_Users.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un perfil para el usuario.",
                        "Perfil requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PerfilUsuario perfilEnum = (PerfilUsuario)comboBox_Pefl_Users.SelectedItem;
                string perfilTextoBD = perfilEnum.ToString().ToUpper();

                string codigoGenerado = GenerarCodigoUsuario();

                Usuario nuevo = new Usuario
                {
                    Codigo = codigoGenerado,
                    Nombre = txtCre_Nombre.Text.Trim(),
                    ApellidoPaterno = txtCre__Apell_P.Text.Trim(),
                    ApellidoMaterno = txtCre__Apell_M.Text.Trim(),
                    Perfil = perfilTextoBD
                };

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
        // 🟣 TAB 3 — EDITAR USUARIO (CON USE CASE)
        // ============================================
        private void btn_Modi_User_Click(object sender, EventArgs e)
        {
            if (_usuarioEnEdicion == null)
            {
                MessageBox.Show("No hay usuario cargado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _usuarioEnEdicion.Nombre = TextBoxMod_Nombre.Text.Trim();
                _usuarioEnEdicion.ApellidoPaterno = TextBoxMod_Apell_P.Text.Trim();
                _usuarioEnEdicion.ApellidoMaterno = TextBoxMod_Apell_M.Text.Trim();

                PerfilUsuario perfilEnum = (PerfilUsuario)comboBox_Mod_Perfil.SelectedItem;
                _usuarioEnEdicion.Perfil = perfilEnum.ToString().ToUpper();

                string passAnterior = Box_Edit_password_after.PasswordValue.Trim();
                string passNueva = Box_Edit_password.PasswordValue.Trim();
                string passNuevaRepetida = Box_Edit_Repit_password.PasswordValue.Trim();

                bool actualizado = _actualizarUsuarioUseCase.Ejecutar(
                    _usuarioEnEdicion,
                    passAnterior,
                    passNueva,
                    passNuevaRepetida
                );

                if (!actualizado)
                    throw new Exception("No se pudo actualizar el usuario.");

                MessageBox.Show("Usuario actualizado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                userTable.CargarDatos();
                tabConfiguraciones.SelectedTab = tab_PanelAdministracionU;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar usuario: " + ex.Message);
            }
        }

        // ============================================
        // ⚙️ MÉTODOS PRIVADOS
        // ============================================
        private void CargarPerfilesUsuarios()
        {
            comboBox_Pefl_Users.DataSource = Enum.GetValues(typeof(PerfilUsuario));
        }

        private void CargarPerfilesModificacion()
        {
            comboBox_Mod_Perfil.DataSource = Enum.GetValues(typeof(PerfilUsuario));
        }

        private string GenerarCodigoUsuario()
        {
            // 1. Obtener los textos de los inputs, quitar espacios sobrantes y convertir a mayúsculas
            string nombre = txtCre_Nombre.Text.Trim().ToUpper();

            // Al Paterno le quitamos los espacios intermedios por si es compuesto (ej: "DE LA O" -> "DELAO")
            // para que el ID no tenga espacios vacíos.
            string apPaterno = txtCre__Apell_P.Text.Trim().Replace(" ", "").ToUpper();

            string apMaterno = txtCre__Apell_M.Text.Trim().ToUpper();

            // 2. Obtener las iniciales (validamos que no esté vacío para evitar error de índice)
            string letraNombre = (nombre.Length > 0) ? nombre.Substring(0, 1) : "X";
            string letraMaterno = (apMaterno.Length > 0) ? apMaterno.Substring(0, 1) : "X";

            // 3. Armar el código: USR + InicialNombre + PaternoCompleto + InicialMaterno
            // Ejemplo: Juan Perez Lopez -> USRJPEREZL
            Random rnd = new Random();
            string aleatorio = rnd.Next(10, 99).ToString(); // Genera número entre 10 y 99
            string codigoGenerado = $"USR_{letraNombre}{apPaterno}{letraMaterno}{aleatorio}";

            // 4. Validación de seguridad: Tu base de datos es VARCHAR2(20). 
            // Si el apellido es muy largo, cortamos para que no de error al guardar.
            if (codigoGenerado.Length > 20)
            {
                codigoGenerado = codigoGenerado.Substring(0, 20);
            }

            return codigoGenerado;
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