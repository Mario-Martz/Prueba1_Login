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
        // Remueve esta línea: private readonly ProcesarCalendarioUseCase _useCase = new();
        // En su lugar, declara sin inicializar
        private CargarCalendarioUseCase? _useCase;

        public Volantas_Home(int tabIndex = 0)
        {
            InitializeComponent();

            // Inicializar el caso de uso cuando se necesite
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
                using var conn = DatabaseConnection.GetConnection();
                string connectionString = conn.ConnectionString;

                var repository = new CalendarioRepository(connectionString);
                _useCase = new CargarCalendarioUseCase(repository);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ============================================
        // 🔵 MÉTODOS GENERALES DEL FORM
        // ============================================
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
            // Configurar DataGridView llamado dataView_Calendario
            if (dataView_Calendario == null)
            {
                MessageBox.Show("DataGridView 'dataView_Calendario' no encontrado", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataView_Calendario.AutoGenerateColumns = false;
            dataView_Calendario.Columns.Clear();

            // Configurar columnas
            var columnas = new[]
            {
                new { Name = "IdTipoSorteo", Header = "Tipo Sort.", Width = 70 },
                new { Name = "NumeroSorteo", Header = "Núm. Sort.", Width = 80 },
                new { Name = "NumeroInterno", Header = "Interno", Width = 70 },
                new { Name = "EmisionSerie", Header = "Emisión", Width = 90 },
                new { Name = "NumeroSeries", Header = "Series", Width = 60 },
                new { Name = "FechaCelebracion", Header = "Fecha", Width = 100 },
                new { Name = "ValorEntero", Header = "Valor", Width = 80 },
                new { Name = "Estatus", Header = "Estatus", Width = 60 },
                new { Name = "BilleteInicial", Header = "Billete Inic.", Width = 100 },
                new { Name = "BilleteFinal", Header = "Billete Fin.", Width = 100 }
            };

            foreach (var col in columnas)
            {
                var dataColumn = new DataGridViewTextBoxColumn
                {
                    Name = col.Name,
                    HeaderText = col.Header,
                    DataPropertyName = col.Name,
                    Width = col.Width
                };

                // Formato para columnas específicas
                if (col.Name == "ValorEntero")
                {
                    dataColumn.DefaultCellStyle.Format = "N2";
                }
                else if (col.Name == "FechaCelebracion")
                {
                    dataColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                dataView_Calendario.Columns.Add(dataColumn);
            }
        }

        // =====================================================
        // 🔵 ABRIR ARCHIVO Y PROCESAR
        // =====================================================
        private void btn_CargraCalendario_Click(object sender, EventArgs e)
        {
            try
            {
                if (_useCase == null)
                {
                    MessageBox.Show("No se pudo inicializar el sistema. Verifique la conexión a la base de datos.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Archivos TXT|*.txt";
                dialog.Title = "Seleccionar archivo CALENDARIO.txt";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                // Mostrar mensaje de carga
                Cursor.Current = Cursors.WaitCursor;
                this.Enabled = false;

                // Mostrar información del archivo
                MostrarInfoArchivo(dialog.FileName);

                // Procesar archivo
                var lista = _useCase.Ejecutar(dialog.FileName);

                // Mostrar datos en el DataGridView
                dataView_Calendario.DataSource = ConvertirListaATabla(lista);

                MessageBox.Show($"Calendario cargado correctamente.\n" +
                               $"Registros procesados: {lista.Count}\n" +
                               $"Archivo: {Path.GetFileName(dialog.FileName)}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el calendario:\n\n{ex.Message}\n\n" +
                               $"Tipo de error: {ex.GetType().Name}\n" +
                               $"¿Es problema de formato? Intente verificar el formato del archivo.",
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
                Console.WriteLine($"=== INFORMACIÓN DEL ARCHIVO ===");
                Console.WriteLine($"Ruta: {rutaArchivo}");
                Console.WriteLine($"Total líneas: {lineas.Length}");

                if (lineas.Length > 0)
                {
                    Console.WriteLine($"Primera línea: {lineas[0]}");
                    Console.WriteLine($"Longitud primera línea: {lineas[0].Length}");

                    // Mostrar primeros 3 caracteres de cada línea para identificar formato
                    Console.WriteLine("Primeros caracteres de cada línea:");
                    for (int i = 0; i < Math.Min(5, lineas.Length); i++)
                    {
                        if (!string.IsNullOrEmpty(lineas[i]))
                        {
                            string primeros = lineas[i].Length > 20 ? lineas[i].Substring(0, 20) + "..." : lineas[i];
                            Console.WriteLine($"  Línea {i + 1}: {primeros}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer archivo: {ex.Message}");
            }
        }

        // =====================================================
        // 🔵 Convertir lista a DataTable
        // =====================================================
        private DataTable ConvertirListaATabla(List<CalendarioSorteos> lista)
        {
            DataTable dt = new DataTable();

            // Definir columnas según la tabla
            dt.Columns.Add("IdTipoSorteo", typeof(int));
            dt.Columns.Add("NumeroSorteo", typeof(int));
            dt.Columns.Add("NumeroInterno", typeof(int));
            dt.Columns.Add("EmisionSerie", typeof(long));
            dt.Columns.Add("NumeroSeries", typeof(int));
            dt.Columns.Add("FechaCelebracion", typeof(DateTime));
            dt.Columns.Add("ValorEntero", typeof(decimal));
            dt.Columns.Add("Estatus", typeof(string));
            dt.Columns.Add("BilleteInicial", typeof(long));
            dt.Columns.Add("BilleteFinal", typeof(long));

            foreach (var item in lista)
            {
                dt.Rows.Add(
                    item.IdTipoSorteo,
                    item.NumeroSorteo,
                    item.NumeroInterno ?? 0,
                    item.EmisionSerie ?? 0,
                    item.NumeroSeries ?? 0,
                    item.FechaCelebracion,
                    item.ValorEntero ?? 0m,
                    item.Estatus,
                    item.BilleteInicial ?? 0,
                    item.BilleteFinal ?? 0
                );
            }

            return dt;
        }

    }
}