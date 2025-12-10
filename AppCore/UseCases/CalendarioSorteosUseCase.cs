using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using System.Globalization;
using System.Windows.Forms;

namespace Prueba1_Login.Domain.UseCases
{
    public class CargarCalendarioUseCase
    {
        private readonly ICalendarioSorteosRepository _repository;

        public ICalendarioSorteosRepository Repository => _repository;

        public CargarCalendarioUseCase(ICalendarioSorteosRepository repository)
        {
            _repository = repository;
        }

        // =============================================
        // 🔵 MÉTODO PRINCIPAL: CARGAR ARCHIVO
        // =============================================
        public List<CalendarioSorteos> Ejecutar(string rutaArchivo)
        {
            // 1. Verificar si hay datos existentes
            int existentes = _repository.ContarRegistros();

            if (existentes > 0)
            {
                var resp = MessageBox.Show(
                    $"La tabla CALENDARIO_SORTEOS contiene {existentes} registros.\n\n" +
                    $"¿Deseas ELIMINARLOS antes de cargar el nuevo archivo?",
                    "Datos existentes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resp == DialogResult.Yes)
                {
                    _repository.EliminarTodo();
                }
            }

            // 2. Cargar archivo
            var lineas = File.ReadAllLines(rutaArchivo);
            var lista = ParsearLineas(lineas);

            // 3. Insertar nueva lista
            _repository.InsertarLista(lista);

            return lista;
        }

        // =============================================
        // 🔵 PARSEO DE LÍNEAS
        // =============================================
        private List<CalendarioSorteos> ParsearLineas(string[] lineas)
        {
            var lista = new List<CalendarioSorteos>();

            foreach (var linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                if (linea.Length < 57)
                    throw new Exception($"La línea no cumple los 57 caracteres: (len {linea.Length}) {linea}");

                lista.Add(ParsearLinea(linea));
            }

            return lista;
        }

        private CalendarioSorteos ParsearLinea(string linea)
        {
            // Extraer parte útil (0..50)
            string s = linea.Substring(0, 51);

            // Substrings según longitud correcta
            string txtIdTipo = s.Substring(0, 2);
            string txtNumSorteo = s.Substring(2, 4);
            string txtInterno = s.Substring(6, 2);
            string txtEmision = s.Substring(8, 6);
            string txtNumSeries = s.Substring(14, 2);
            string txtFecha = s.Substring(16, 10);
            string txtValor = s.Substring(26, 6);
            string txtEstatus = s.Substring(32, 1);
            string txtBilleteInicial = s.Substring(33, 9);
            string txtBilleteFinal = s.Substring(42, 9);

            return new CalendarioSorteos
            {
                IdTipoSorteo = SafeInt(txtIdTipo),
                NumeroSorteo = SafeInt(txtNumSorteo),
                NumeroInterno = SafeNullableInt(txtInterno),

                EmisionSerie = SafeLong(txtEmision) * 1000L,

                NumeroSeries = SafeNullableInt(txtNumSeries),

                FechaCelebracion = SafeDate(txtFecha) ?? DateTime.MinValue,

                ValorEntero = SafeDecimal(txtValor),

                Estatus = txtEstatus.Trim(),

                BilleteInicial = SafeLong(txtBilleteInicial),
                BilleteFinal = SafeLong(txtBilleteFinal)
            };
        }

        // =============================================
        // 🔵 CONVERSORES SEGUROS
        // =============================================
        private int SafeInt(string s) =>
            int.TryParse(s.Trim(), out var v) ? v : 0;

        private int? SafeNullableInt(string s) =>
            int.TryParse(s.Trim(), out var v) ? (int?)v : null;

        private long SafeLong(string s) =>
            long.TryParse(s.Trim(), out var v) ? v : 0;

        private decimal SafeDecimal(string s) =>
            decimal.TryParse(s.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var v) ? v : 0m;

        private DateTime? SafeDate(string s)
        {
            if (DateTime.TryParseExact(s.Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
                return d;

            string clean = s.Trim();
            if (DateTime.TryParse(clean, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
                return d;

            return null;
        }
    }
}
