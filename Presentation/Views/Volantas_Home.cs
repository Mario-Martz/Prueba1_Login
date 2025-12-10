using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.UseCases;
using Prueba1_Login.Infra.Repository;
using Prueba1_Login.Infrastructure.Data;
using Prueba1_Login.Resources.Fonts_Personalizados;
using System.Data;

namespace Prueba1_Login.Views
{
    public partial class Volantas_Home : Form
    {
        private CargarCalendarioUseCase? _useCase;

        public Volantas_Home(int tabIndex = 0)
        {
            InitializeComponent();

            InicializarUseCase();

            FontManager.Initialize();
            AplicarFuenteAControles(this);

            tabVolantas.ItemSize = new Size(0, 1);
            tabVolantas.SizeMode = TabSizeMode.Fixed;

            for (int i = tabVolantas.TabPages.Count - 1; i >= 0; i--)
            {
                if (i != tabIndex)
                    tabVolantas.TabPages.RemoveAt(i);
            }

            tabVolantas.SelectedIndex = 0;

            ConfigurarDataGridView();
        }

        private void InicializarUseCase()
        {
            try
            {
                string connectionString = DatabaseConnection.ConnectionString;
                var repository = new CalendarioRepository(connectionString);

                _useCase = new CargarCalendarioUseCase(repository);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFuenteAControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Font = FontManager.GetFont(AppFont.MontserratRegular, 18f);
                else if (ctrl is Button btn)
                    btn.Font = FontManager.GetFont(AppFont.MontserratBold, 22f);
                else if (ctrl is TextBox txt)
                    txt.Font = FontManager.GetFont(AppFont.MontserratRegular, 14f);

                if (ctrl.HasChildren)
                    AplicarFuenteAControles(ctrl);
            }
        }

        private void ConfigurarDataGridView()
        {
            
            dataView_Calendario.RowHeadersVisible = false;   // ⬅ Oculta el asterisco
            dataView_Calendario.AllowUserToAddRows = false;  // ⬅ Evita fila de inserción manual
            dataView_Calendario.AutoGenerateColumns = false;
            dataView_Calendario.Columns.Clear();

            var columnas = new[]
            {
                new { Name = "IdTipoSorteo", Header = "IdTipo Sorteo.", Width = 110 },
                new { Name = "NumeroSorteo", Header = "Núm. Sorteo.", Width = 100 },
                new { Name = "NumeroInterno", Header = "NúmInterno", Width = 83 },
                new { Name = "EmisionSerie", Header = "EmisiónSerie", Width = 95 },
                new { Name = "NumeroSeries", Header = "Núm. Series", Width = 95 },
                new { Name = "FechaCelebracion", Header = "Fecha Celebracion", Width = 135 },
                new { Name = "ValorEntero", Header = "ValorEntero", Width = 90 },
                new { Name = "Estatus", Header = "Estatus", Width = 70 },
                new { Name = "BilleteInicial", Header = "Billete Inic.", Width = 90 },
                new { Name = "BilleteFinal", Header = "Billete Fin.", Width = 90 }
            };

            foreach (var col in columnas)
            {
                var c = new DataGridViewTextBoxColumn
                {
                    Name = col.Name,
                    HeaderText = col.Header,
                    DataPropertyName = col.Name,
                    Width = col.Width
                };

                if (col.Name == "ValorEntero")
                {
                    // ✔ Mostrar sin decimales (ej: 800 en vez de 8.00)
                    c.DefaultCellStyle.Format = "N0";
                }
                else if (col.Name == "FechaCelebracion")
                {
                    c.DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                dataView_Calendario.Columns.Add(c);
            }
        }

        private void btn_CargraCalendario_Click(object sender, EventArgs e)
        {
            try
            {
                if (_useCase == null)
                {
                    MessageBox.Show("Error al iniciar subsistema.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ==================================
                // VERIFICAR SI LA TABLA TIENE DATOS
                // ==================================
                int registros = _useCase.Repository.ContarRegistros();

                if (registros > 0)
                {
                    var resp = MessageBox.Show(
                        $"La tabla contiene {registros} registros.\n¿Deseas eliminarlos antes de cargar?",
                        "Confirmar limpieza",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (resp == DialogResult.Yes)
                    {
                        _useCase.Repository.EliminarTodo();
                        MessageBox.Show("Registros eliminados correctamente.", "OK",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Carga cancelada.", "Cancelado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // ===============================
                // ABRIR ARCHIVO TXT
                // ===============================
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Archivos TXT|*.txt";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;

                MostrarInfoArchivo(dialog.FileName);

                var lista = _useCase.Ejecutar(dialog.FileName);

                dataView_Calendario.DataSource = ConvertirListaATabla(lista);

                MessageBox.Show($"Calendario cargado correctamente.\nRegistros: {lista.Count}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
            }
        }

        private void MostrarInfoArchivo(string rutaArchivo)
        {
            try
            {
                var lineas = File.ReadAllLines(rutaArchivo);
                Console.WriteLine($"Archivo: {rutaArchivo}");
                Console.WriteLine($"Líneas: {lineas.Length}");

                if (lineas.Length > 0)
                {
                    Console.WriteLine($"Primera línea ({lineas[0].Length} chars): {lineas[0]}");
                }
            }
            catch { }
        }

        private DataTable ConvertirListaATabla(List<CalendarioSorteos> lista)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdTipoSorteo", typeof(int));
            dt.Columns.Add("NumeroSorteo", typeof(int));
            dt.Columns.Add("NumeroInterno", typeof(int));
            dt.Columns.Add("EmisionSerie", typeof(long));
            dt.Columns.Add("NumeroSeries", typeof(int));
            dt.Columns.Add("FechaCelebracion", typeof(DateTime));
            dt.Columns.Add("ValorEntero", typeof(int)); // ✔ Ya no decimal
            dt.Columns.Add("Estatus", typeof(string));
            dt.Columns.Add("BilleteInicial", typeof(long));
            dt.Columns.Add("BilleteFinal", typeof(long));

            foreach (var x in lista)
            {
                dt.Rows.Add(
                    x.IdTipoSorteo,
                    x.NumeroSorteo,
                    x.NumeroInterno ?? 0,
                    x.EmisionSerie ?? 0,
                    x.NumeroSeries ?? 0,
                    x.FechaCelebracion,
                    x.ValorEntero ?? 0,   // ✔ Se muestra como 800, no 8.00
                    x.Estatus,
                    x.BilleteInicial ?? 0,
                    x.BilleteFinal ?? 0
                );
            }

            return dt;
        }
    }
}
