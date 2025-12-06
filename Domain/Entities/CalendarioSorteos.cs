namespace Prueba1_Login.Domain.Entities
{
    public class CalendarioSorteos
    {
        public int IdTipoSorteo { get; set; }
        public int NumeroSorteo { get; set; }
        public int? NumeroInterno { get; set; }
        public long? EmisionSerie { get; set; }
        public int? NumeroSeries { get; set; }
        public DateTime FechaCelebracion { get; set; }
        public decimal? ValorEntero { get; set; }
        public string Estatus { get; set; } = string.Empty;
        public long? BilleteInicial { get; set; }
        public long? BilleteFinal { get; set; }
    }
}