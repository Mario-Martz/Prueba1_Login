using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Infrastructure.Data.Repositories;
using Prueba1_Login.AppCore.Session;
using System.ComponentModel;

namespace Prueba1_Login.Presentation.Components
{
    [DesignerCategory("Code")]
    public class UserTableControl : UserControl
    {
        public event Action<Usuario>? OnEdit;
        public event Action<Usuario>? OnDelete;

        private DataGridView dgvUsuarios;
        private List<Usuario> usuarios = new();

        public UserTableControl()
        {
            InitializeComponent();
            ConfigurarTabla();
            this.Resize += (s, e) => AjustarColumnas();
        }

        // -----------------------------------------------------
        // INIT
        // -----------------------------------------------------
        private void InitializeComponent()
        {
            dgvUsuarios = new DataGridView();
            ((ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();

            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(0, 0);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(600, 300);

            Controls.Add(dgvUsuarios);

            Name = "UserTableControl";
            Size = new Size(600, 300);

            ((ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        // -----------------------------------------------------
        // CONFIG TABLA
        // -----------------------------------------------------
        private void ConfigurarTabla()
        {
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowTemplate.Height = 48;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvUsuarios.CellPainting += dgvUsuarios_CellPainting;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;

            InicializarColumnas();
        }

        private void InicializarColumnas()
        {
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add("Codigo", "Código");
            dgvUsuarios.Columns.Add("Nombre", "Nombre");
            dgvUsuarios.Columns.Add("ApellidoPaterno", "Apellido Paterno");
            dgvUsuarios.Columns.Add("ApellidoMaterno", "Apellido Materno");
            dgvUsuarios.Columns.Add("Perfil", "Perfil");

            DataGridViewTextBoxColumn colAcciones = new()
            {
                Name = "Acciones",
                HeaderText = "Acciones",
                Width = 90
            };

            dgvUsuarios.Columns.Add(colAcciones);

            AjustarColumnas();
        }

        private void AjustarColumnas()
        {
            foreach (DataGridViewColumn c in dgvUsuarios.Columns)
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvUsuarios.Columns["Acciones"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsuarios.Columns["Acciones"].Width = 90;
        }

        // -----------------------------------------------------
        // CARGAR DATOS
        // -----------------------------------------------------
        public void CargarUsuarios(List<Usuario> lista)
        {
            usuarios = lista ?? new();

            dgvUsuarios.Rows.Clear();

            foreach (var u in usuarios)
            {
                int index = dgvUsuarios.Rows.Add(
                    u.Codigo,
                    u.Nombre,
                    u.ApellidoPaterno,
                    u.ApellidoMaterno,
                    u.Perfil
                );

                dgvUsuarios.Rows[index].Tag = u;
            }
        }

        // -----------------------------------------------------
        // PINTAR ICONOS (editar / eliminar)
        // -----------------------------------------------------
        private void dgvUsuarios_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvUsuarios.Columns[e.ColumnIndex].Name != "Acciones") return;

            e.Handled = true;

            e.PaintBackground(e.CellBounds, true);

            int iconSize = 20;
            int padding = 8;

            Rectangle btnEditar = new Rectangle(
                e.CellBounds.X + padding,
                e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            Rectangle btnEliminar = new Rectangle(
                e.CellBounds.X + padding + iconSize + padding,
                e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            e.Graphics.DrawImage(Resources.Properties.Resources.edit, btnEditar);
            e.Graphics.DrawImage(Resources.Properties.Resources.delete, btnEliminar);
        }

        // -----------------------------------------------------
        // CLICK ICONOS
        // -----------------------------------------------------
        private void dgvUsuarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvUsuarios.Columns[e.ColumnIndex].Name != "Acciones") return;

            Usuario usuario = (Usuario)dgvUsuarios.Rows[e.RowIndex].Tag;

            // ================================
            // 🛑 NO PERMITIR ELIMINAR EL USUARIO LOGUEADO
            // ================================
            if (usuario.Codigo == SessionManager.UsuarioCodigo)
            {
                MessageBox.Show(
                    "No puedes eliminar el usuario activo que está usando el sistema.",
                    "Acción no permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var cellRect = dgvUsuarios.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            int iconSize = 20;
            int padding = 8;

            Rectangle editarRect = new Rectangle(
                cellRect.X + padding,
                cellRect.Y + (cellRect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            Rectangle eliminarRect = new Rectangle(
                cellRect.X + padding + iconSize + padding,
                cellRect.Y + (cellRect.Height - iconSize) / 2,
                iconSize,
                iconSize
            );

            Point cursor = dgvUsuarios.PointToClient(Cursor.Position);

            if (editarRect.Contains(cursor))
                OnEdit?.Invoke(usuario);
            else if (eliminarRect.Contains(cursor))
                OnDelete?.Invoke(usuario);
        }

        // -----------------------------------------------------
        public void Limpiar()
        {
            usuarios.Clear();
            dgvUsuarios.Rows.Clear();
        }

        [Browsable(false)]
        public DataGridView Grid => dgvUsuarios;

        public void CargarDatos()
        {
            try
            {
                var repo = new UsuarioRepository();
                List<Usuario> lista = repo.ObtenerTodos();
                CargarUsuarios(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message);
            }
        }
    }
}