namespace Prueba1_Login.Domain.Entities
{
    public class CalendarioSorteos
    {
        public int IdTipoSorteo { get; set; }
        public int NumeroSorteo { get; set; }
        public int? NumeroInterno { get; set; }
        public long? EmisionSerie { get; set; }    // multiplicado *1000
        public int? NumeroSeries { get; set; }    // ahora 2 dígitos
        public DateTime FechaCelebracion { get; set; } // dd/MM/yyyy
        public decimal? ValorEntero { get; set; } // formato moneda (N2)
        public string Estatus { get; set; } = string.Empty;
        public long? BilleteInicial { get; set; } // 9 dígitos
        public long? BilleteFinal { get; set; }   // 9 dígitos
    }
}