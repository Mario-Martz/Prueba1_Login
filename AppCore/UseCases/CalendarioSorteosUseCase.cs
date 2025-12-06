using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using System.Globalization;

namespace Prueba1_Login.Domain.UseCases
{
    public class CargarCalendarioUseCase
    {
        private readonly ICalendarioSorteosRepository _repo;

        public CargarCalendarioUseCase(ICalendarioSorteosRepository repo)
        {
            _repo = repo;
        }

        public List<CalendarioSorteos> Ejecutar(string rutaTxt)
        {
            var lista = new List<CalendarioSorteos>();

            try
            {
                var lineas = File.ReadAllLines(rutaTxt);

                foreach (var linea in lineas)
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    try
                    {
                        var item = ParsearLinea(linea.Trim());
                        if (item != null)
                        {
                            lista.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Registrar pero continuar
                        Console.WriteLine($"Advertencia: Error en línea '{linea}': {ex.Message}");
                    }
                }

                // Guardar en BD solo si tenemos repositorio y datos
                if (lista.Any() && _repo != null)
                {
                    _repo.InsertarLista(lista);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al procesar archivo: {ex.Message}", ex);
            }
        }

        private CalendarioSorteos? ParsearLinea(string linea)
        {
            // Eliminar espacios y caracteres no deseados
            linea = linea.Trim();

            // Si la línea está vacía o es demasiado corta, ignorar
            if (string.IsNullOrEmpty(linea) || linea.Length < 10)
                return null;

            var item = new CalendarioSorteos();

            try
            {
                // Intentar diferentes formatos

                // FORMATO 1: Con separador de pipe (|)
                if (linea.Contains('|'))
                {
                    return ParsearConPipe(linea);
                }
                // FORMATO 2: Longitud fija (como en el ejemplo anterior)
                else if (linea.Length >= 65)
                {
                    return ParsearLongitudFija(linea);
                }
                // FORMATO 3: Otro formato (como el del error)
                else
                {
                    return ParsearFormatoGenerico(linea);
                }
            }
            catch (Exception ex)
            {
                throw new FormatException($"No se pudo parsear la línea: '{linea}'. Error: {ex.Message}");
            }
        }

        private CalendarioSorteos ParsearConPipe(string linea)
        {
            var partes = linea.Split('|', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length < 8)
                throw new FormatException($"Muy pocas columnas: {partes.Length}");

            var item = new CalendarioSorteos();

            // Mapear según posición
            if (partes.Length > 0) item.IdTipoSorteo = ParsearEntero(partes[0]);
            if (partes.Length > 1) item.NumeroSorteo = ParsearEntero(partes[1]);
            if (partes.Length > 2) item.NumeroInterno = ParsearEnteroOpcional(partes[2]);
            if (partes.Length > 3) item.EmisionSerie = ParsearLongOpcional(partes[3]);
            if (partes.Length > 4) item.NumeroSeries = ParsearEnteroOpcional(partes[4]);
            if (partes.Length > 5) item.FechaCelebracion = ParsearFecha(partes[5]);
            if (partes.Length > 6) item.ValorEntero = ParsearDecimalOpcional(partes[6]);
            if (partes.Length > 7) item.Estatus = ParsearEstatus(partes[7]);
            if (partes.Length > 8) item.BilleteInicial = ParsearLongOpcional(partes[8]);
            if (partes.Length > 9) item.BilleteFinal = ParsearLongOpcional(partes[9]);

            return item;
        }

        private CalendarioSorteos ParsearLongitudFija(string linea)
        {
            // Formato: 022863310000600207/11/2025000800D000000001000060000000040
            var item = new CalendarioSorteos();

            try
            {
                // Posiciones según el formato anterior
                if (linea.Length >= 2)
                    item.IdTipoSorteo = int.Parse(linea.Substring(0, 2));

                if (linea.Length >= 8)
                    item.NumeroSorteo = int.Parse(linea.Substring(2, 6));

                // Buscar fecha en formato dd/MM/yyyy
                for (int i = 0; i <= linea.Length - 10; i++)
                {
                    if (EsFechaValida(linea.Substring(i, 10)))
                    {
                        item.FechaCelebracion = DateTime.ParseExact(
                            linea.Substring(i, 10),
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                        break;
                    }
                }

                // Buscar estatus (D, B, X)
                foreach (char c in new[] { 'D', 'B', 'X' })
                {
                    if (linea.Contains(c))
                    {
                        item.Estatus = c.ToString();
                        break;
                    }
                }

                // Si no se encontró estatus, usar por defecto
                if (string.IsNullOrEmpty(item.Estatus))
                    item.Estatus = "U";

                return item;
            }
            catch
            {
                throw new FormatException("Formato de longitud fija no reconocido");
            }
        }

        private CalendarioSorteos ParsearFormatoGenerico(string linea)
        {
            // Para líneas como: "102255000000000007111202500080000000000100006000000"
            var item = new CalendarioSorteos();

            // Intentar extraer información básica
            // Asumir que los primeros 2 dígitos son IdTipoSorteo
            if (linea.Length >= 2 && int.TryParse(linea.Substring(0, 2), out int idTipo))
                item.IdTipoSorteo = idTipo;

            // Buscar patrones numéricos que podrían ser fechas
            // Buscar secuencia de 8 dígitos que podría ser fecha compacta
            for (int i = 0; i <= linea.Length - 8; i++)
            {
                string segmento = linea.Substring(i, 8);
                if (int.TryParse(segmento, out int posibleFechaNum))
                {
                    // Intentar interpretar como ddmmyyyy
                    if (posibleFechaNum >= 1010000 && posibleFechaNum <= 31129999)
                    {
                        try
                        {
                            string fechaStr = $"{segmento.Substring(0, 2)}/{segmento.Substring(2, 2)}/{segmento.Substring(4, 4)}";
                            if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                            {
                                item.FechaCelebracion = fecha;
                                break;
                            }
                        }
                        catch
                        {
                            // Continuar buscando
                        }
                    }
                }
            }

            // Si no se pudo parsear fecha, usar fecha actual
            if (item.FechaCelebracion == DateTime.MinValue)
                item.FechaCelebracion = DateTime.Today;

            // Buscar estatus
            foreach (char c in new[] { 'D', 'B', 'X', 'N', 'S' })
            {
                if (linea.Contains(c))
                {
                    item.Estatus = c.ToString();
                    break;
                }
            }

            // Valor por defecto
            if (string.IsNullOrEmpty(item.Estatus))
                item.Estatus = "U";

            // Generar número de sorteo si no se pudo extraer
            if (item.NumeroSorteo == 0 && linea.Length >= 8)
            {
                if (int.TryParse(linea.Substring(2, 6), out int numSorteo))
                    item.NumeroSorteo = numSorteo;
            }

            return item;
        }

        // ============================================
        // 🔵 MÉTODOS AUXILIARES DE PARSEO
        // ============================================

        private bool EsFechaValida(string texto)
        {
            return DateTime.TryParseExact(texto, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private int ParsearEntero(string texto)
        {
            texto = texto.Trim();
            if (string.IsNullOrEmpty(texto)) return 0;

            // Eliminar caracteres no numéricos
            string numeros = new string(texto.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(numeros)) return 0;

            return int.Parse(numeros);
        }

        private int? ParsearEnteroOpcional(string texto)
        {
            texto = texto.Trim();
            if (string.IsNullOrEmpty(texto) || texto == "-" || texto.ToLower() == "null")
                return null;

            string numeros = new string(texto.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(numeros)) return null;

            return int.Parse(numeros);
        }

        private long? ParsearLongOpcional(string texto)
        {
            texto = texto.Trim();
            if (string.IsNullOrEmpty(texto) || texto == "-" || texto.ToLower() == "null")
                return null;

            string numeros = new string(texto.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(numeros)) return null;

            return long.Parse(numeros);
        }

        private decimal? ParsearDecimalOpcional(string texto)
        {
            texto = texto.Trim();
            if (string.IsNullOrEmpty(texto) || texto == "-" || texto.ToLower() == "null")
                return null;

            // Manejar formatos como "S800.00 D"
            texto = texto.Replace("S", "").Replace("D", "").Replace("X", "").Replace("B", "").Trim();

            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultado))
                return resultado;

            return null;
        }

        private DateTime ParsearFecha(string texto)
        {
            texto = texto.Trim();

            // Intentar diferentes formatos de fecha
            string[] formatos = { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy", "yyyyMMdd" };

            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(texto, formato, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime fecha))
                {
                    return fecha;
                }
            }

            // Si no se puede parsear, usar fecha actual
            return DateTime.Today;
        }

        private string ParsearEstatus(string texto)
        {
            texto = texto.Trim().ToUpper();

            if (texto.Contains("D")) return "D";
            if (texto.Contains("B")) return "B";
            if (texto.Contains("X")) return "X";
            if (texto.Contains("N")) return "N";
            if (texto.Contains("S")) return "S";

            return "U"; // Desconocido
        }
    }
}