using Prueba1_Login.AppCore.Session;
using Prueba1_Login.Presentation.Interfaces;
using Prueba1_Login.Presentation.Presenters;
using Prueba1_Login.Resources.Fonts_Personalizados;
using Prueba1_Login.Views;

namespace Prueba1_Login.Froms
{
    public partial class Dashboard : Form, IDashboardView
    {
        private readonly DashboardPresenter presenter;
        private Form activeForm = null;

        public Dashboard()
        {
            InitializeComponent();
            customizeDesign();
            AplicarFuenteMontserrat();

            presenter = new DashboardPresenter(this);

            // 🔥 APLICAR PERMISOS AL CARGAR
            this.Load += Dashboard_Load;
        }

        private void Dashboard_Load(object? sender, EventArgs e)
        {
            AplicarPermisos();
        }

        // ===========================================================
        // 🔥 PERMISOS BASADOS EN PERFIL DESDE BD
        // ===========================================================
        private void AplicarPermisos()
        {
            string perfil = SessionManager.Perfil?.Trim().ToUpper() ?? "";

            // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
            // 🔥 SOLO SISTEMAS PUEDE VER EL BOTÓN "USUARIOS"
            // ▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
            if (perfil != "SISTEMAS")
            {
                btn_usuarios.Enabled = false;
                btn_usuarios.BackColor = Color.Gray;
                btn_usuarios.ForeColor = Color.DarkGray;
                btn_usuarios.Cursor = Cursors.No;
            }

            // ▬▬▬ EJEMPLO: INVITADO NO PUEDE VER NADA DE CONFIGURACIÓN
            if (perfil == "INVITADO")
            {
                btn_parametros.Enabled = false;
                btn_parametros.BackColor = Color.Gray;
                btn_parametros.ForeColor = Color.DarkGray;
                btn_parametros.Cursor = Cursors.No;
            }

            // ▬▬▬ EJEMPLO: USUARIOS no acceden a Catálogos
            if (perfil == "USUARIOS")
            {
                btn_tipo_Sorteo.Enabled = false;
                btn_Sig_Zodiaco.Enabled = false;
            }
        }

        // ===========================================================
        // MENÚ Y RESTO DE TU DASHBOARD (SIN CAMBIOS)
        // ===========================================================

        private void customizeDesign()
        {
            panel_SubMenu_btnVolantas.Visible = false;
            panel_SubMenu_btnCatalogos.Visible = false;
            panel_SubMenu_btnConfiguraciones.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panel_SubMenu_btnVolantas.Visible)
                panel_SubMenu_btnVolantas.Visible = false;

            if (panel_SubMenu_btnCatalogos.Visible)
                panel_SubMenu_btnCatalogos.Visible = false;

            if (panel_SubMenu_btnConfiguraciones.Visible)
                panel_SubMenu_btnConfiguraciones.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (!subMenu.Visible)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btn_Volantas_Click(object sender, EventArgs e)
            => showSubMenu(panel_SubMenu_btnVolantas);

        private void btn_Catalogos_Click(object sender, EventArgs e)
            => showSubMenu(panel_SubMenu_btnCatalogos);

        private void btn_configuracion_Click(object sender, EventArgs e)
            => showSubMenu(panel_SubMenu_btnConfiguraciones);

        private void btn_Estructura_premios_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Volantas_Home(0));
            hideSubMenu();
        }

        private void btn_Calendario_sorteos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Volantas_Home(1));
            hideSubMenu();
        }

        private void btn_Loteria_tradicional_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Volantas_Home(2));
            hideSubMenu();
        }

        private void btn_tipo_Sorteo_Click(object sender, EventArgs e)
            => hideSubMenu();

        private void btn_Sig_Zodiaco_Click(object sender, EventArgs e)
            => hideSubMenu();

        private void btn_parametros_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Configuracion_Home(0));
            hideSubMenu();
        }

        private void btn_usuarios_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Configuracion_Home(1));
            hideSubMenu();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            presenter.EjecutarSalida();
        }

        private void OpenChildForm(Form childFrom)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childFrom;
            childFrom.TopLevel = false;
            childFrom.FormBorderStyle = FormBorderStyle.None;
            childFrom.Dock = DockStyle.Fill;

            panel_FormHijo.Controls.Add(childFrom);
            panel_FormHijo.Tag = childFrom;

            childFrom.BringToFront();
            childFrom.Show();
        }

        private void AplicarFuenteMontserrat()
        {
            try
            {
                FontManager.Initialize();
                Font fuente = FontManager.GetFont(AppFont.MontserratRegular, 18f);
                AplicarFuenteAControles(this, fuente);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠️ No se pudieron aplicar las fuentes Montserrat:\n{ex.Message}");
            }
        }

        private void AplicarFuenteAControles(Control contenedor, Font fuente)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl is Button || ctrl is Label)
                    ctrl.Font = fuente;

                if (ctrl.HasChildren)
                    AplicarFuenteAControles(ctrl, fuente);
            }
        }

        public void CerrarAplicacion() => Application.Exit();

        public void MostrarMensaje(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public DialogResult MostrarConfirmacion(string mensaje, string titulo)
        {
            return MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        }
    }
}