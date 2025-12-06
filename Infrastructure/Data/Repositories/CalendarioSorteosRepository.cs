using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Prueba1_Login.Infra.Repository
{
    public class CalendarioRepository : ICalendarioSorteosRepository
    {
        private readonly string _connectionString;

        public CalendarioRepository(string conn)
        {
            _connectionString = conn;
        }

        public void InsertarLista(IEnumerable<CalendarioSorteos> items)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();

                foreach (var x in items)
                {
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                    INSERT INTO CALENDARIO_SORTEOS 
                    (ID_TIPO_SORTEO, NUMERO_SORTEO, NUMERO_INTERNO, EMISION_SERIE,
                     NUMERO_SERIES, FECHA_CELEBRACION, VALOR_ENTERO, ESTATUS,
                     BILLETE_INICIAL, BILLETE_FINAL)
                    VALUES
                    (:p1, :p2, :p3, :p4, :p5, :p6, :p7, :p8, :p9, :p10)";

                    cmd.Parameters.Add(":p1", x.IdTipoSorteo);
                    cmd.Parameters.Add(":p2", x.NumeroSorteo);
                    cmd.Parameters.Add(":p3", (object)x.NumeroInterno ?? DBNull.Value);
                    cmd.Parameters.Add(":p4", (object)x.EmisionSerie ?? DBNull.Value);
                    cmd.Parameters.Add(":p5", (object)x.NumeroSeries ?? DBNull.Value);
                    cmd.Parameters.Add(":p6", x.FechaCelebracion);
                    cmd.Parameters.Add(":p7", (object)x.ValorEntero ?? DBNull.Value);
                    cmd.Parameters.Add(":p8", x.Estatus);
                    cmd.Parameters.Add(":p9", (object)x.BilleteInicial ?? DBNull.Value);
                    cmd.Parameters.Add(":p10", (object)x.BilleteFinal ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<CalendarioSorteos> Buscar(DateTime inicio, DateTime fin, int tipoSorteo)
        {
            // Luego te genero esta parte si la necesitas.
            throw new NotImplementedException();
        }
    }
}
