using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace Prueba1_Login.Infra.Repository
{
    public class CalendarioRepository : ICalendarioSorteosRepository
    {
        private readonly string _connectionString;

        public CalendarioRepository(string conn)
        {
            _connectionString = conn;
        }

        // ============================
        //  BORRAR TODA LA TABLA
        // ============================
        public void EliminarTodo()
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CALENDARIO_SORTEOS";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ============================
        //  CONTAR REGISTROS
        // ============================
        public int ContarRegistros()
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM CALENDARIO_SORTEOS";
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // ============================
        //  GUARDAR / MERGE
        // ============================
        public void GuardarCalendario(CalendarioSorteos x)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                MERGE INTO CALENDARIO_SORTEOS dst
                USING (
                    SELECT 
                        :p1 AS ID_TIPO_SORTEO,
                        :p2 AS NUMERO_SORTEO,
                        :p3 AS NUMERO_INTERNO,
                        :p4 AS EMISION_SERIE,
                        :p5 AS NUMERO_SERIES,
                        :p6 AS FECHA_CELEBRACION,
                        :p7 AS VALOR_ENTERO,
                        :p8 AS ESTATUS,
                        :p9 AS BILLETE_INICIAL,
                        :p10 AS BILLETE_FINAL
                    FROM DUAL
                ) src
                ON (dst.ID_TIPO_SORTEO = src.ID_TIPO_SORTEO
                    AND dst.NUMERO_SORTEO = src.NUMERO_SORTEO)
                WHEN MATCHED THEN 
                    UPDATE SET
                        dst.NUMERO_INTERNO = src.NUMERO_INTERNO,
                        dst.EMISION_SERIE = src.EMISION_SERIE,
                        dst.NUMERO_SERIES = src.NUMERO_SERIES,
                        dst.FECHA_CELEBRACION = src.FECHA_CELEBRACION,
                        dst.VALOR_ENTERO = src.VALOR_ENTERO,
                        dst.ESTATUS = src.ESTATUS,
                        dst.BILLETE_INICIAL = src.BILLETE_INICIAL,
                        dst.BILLETE_FINAL = src.BILLETE_FINAL
                WHEN NOT MATCHED THEN
                    INSERT (
                        ID_TIPO_SORTEO, NUMERO_SORTEO, NUMERO_INTERNO, EMISION_SERIE,
                        NUMERO_SERIES, FECHA_CELEBRACION, VALOR_ENTERO, ESTATUS,
                        BILLETE_INICIAL, BILLETE_FINAL
                    )
                    VALUES (
                        src.ID_TIPO_SORTEO, src.NUMERO_SORTEO, src.NUMERO_INTERNO, src.EMISION_SERIE,
                        src.NUMERO_SERIES, src.FECHA_CELEBRACION, src.VALOR_ENTERO, src.ESTATUS,
                        src.BILLETE_INICIAL, src.BILLETE_FINAL
                    )";

                    cmd.Parameters.Add(new OracleParameter("p1", x.IdTipoSorteo));
                    cmd.Parameters.Add(new OracleParameter("p2", x.NumeroSorteo));
                    cmd.Parameters.Add(new OracleParameter("p3", (object?)x.NumeroInterno ?? DBNull.Value));
                    cmd.Parameters.Add(new OracleParameter("p4", (object?)x.EmisionSerie ?? DBNull.Value));
                    cmd.Parameters.Add(new OracleParameter("p5", (object?)x.NumeroSeries ?? DBNull.Value));
                    cmd.Parameters.Add(new OracleParameter("p6", x.FechaCelebracion));
                    cmd.Parameters.Add(new OracleParameter("p7", x.ValorEntero ?? 0m));
                    cmd.Parameters.Add(new OracleParameter("p8", x.Estatus ?? " "));
                    cmd.Parameters.Add(new OracleParameter("p9", (object?)x.BilleteInicial ?? DBNull.Value));
                    cmd.Parameters.Add(new OracleParameter("p10", (object?)x.BilleteFinal ?? DBNull.Value));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarLista(IEnumerable<CalendarioSorteos> items)
        {
            using (var conn = new OracleConnection(_connectionString))
            {
                conn.Open();
                foreach (var x in items)
                    GuardarCalendario(x);
            }
        }

        public IEnumerable<CalendarioSorteos> Buscar(DateTime inicio, DateTime fin, int tipoSorteo)
        {
            throw new NotImplementedException();
        }
    }
}
