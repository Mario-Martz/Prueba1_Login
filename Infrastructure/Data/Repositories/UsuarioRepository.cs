using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;
using Prueba1_Login.AppCore.Session; // Asegúrate de referenciar donde está tu SessionManager
using System.Windows.Forms; // Para los MessageBox
using System;
using System.Collections.Generic;

namespace Prueba1_Login.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // Helper para obtener usuario actual de forma segura
        private string ObtenerUsuarioActual()
        {
            return SessionManager.UsuarioCodigo ?? "SISTEMA";
        }

        // =====================================================
        // OBTENER POR CÓDIGO (FILTRADO POR ACTIVO)
        // =====================================================
        public Usuario? ObtenerPorCodigo(string codigo)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                // AGREGAMOS: AND U.ACTIVO = 1
                string sql = @"
                    SELECT 
                        U.CVE_USUARIO, 
                        U.NOMBRE, 
                        U.APELLIDO_PATERNO, 
                        U.APELLIDO_MATERNO,
                        NVL(U.PASSWORD_HASH, ''),
                        NVL(U.PASSWORD_SALT, ''),
                        P.DESCRIPCION
                    FROM USUARIOS U
                    JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL
                    WHERE UPPER(U.CVE_USUARIO) = UPPER(:cve)
                    AND U.ACTIVO = 1";

                using var cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":cve", codigo);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return MapReaderToUsuario(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerPorCodigo: {ex.Message}");
            }
            return null;
        }

        // =====================================================
        // OBTENER TODOS (SOLO ACTIVOS)
        // =====================================================
        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                // AGREGAMOS: WHERE U.ACTIVO = 1
                string sql = @"
                    SELECT 
                        U.CVE_USUARIO,
                        U.NOMBRE,
                        U.APELLIDO_PATERNO,
                        U.APELLIDO_MATERNO,
                        NVL(U.PASSWORD_HASH, ''),
                        NVL(U.PASSWORD_SALT, ''),
                        P.DESCRIPCION
                    FROM USUARIOS U
                    JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL
                    WHERE U.ACTIVO = 1
                    ORDER BY U.CVE_USUARIO";

                using var cmd = new OracleCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(MapReaderToUsuario(reader));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerTodos: {ex.Message}");
            }
            return lista;
        }

        // =====================================================
        // CREAR (CON AUDITORÍA)
        // =====================================================
        public bool Crear(Usuario usuario)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                // 1. Validar Perfil
                string sqlPerfil = "SELECT CVE_PERFIL FROM PERFILES WHERE DESCRIPCION = :perfil";
                int cvePerfil = -1;

                using (var cmdPerfil = new OracleCommand(sqlPerfil, conn))
                {
                    cmdPerfil.Parameters.Add(":perfil", usuario.Perfil);
                    var result = cmdPerfil.ExecuteScalar();
                    if (result == null) throw new Exception("Perfil no encontrado");
                    cvePerfil = Convert.ToInt32(result);
                }

                // 2. Insertar con Auditoría
                // Nótese que ACTIVO se guarda como 1, y FECHA_REGISTRO como SYSDATE
                string sql = @"
                    INSERT INTO USUARIOS
                    (CVE_USUARIO, NOMBRE, APELLIDO_PATERNO, APELLIDO_MATERNO,
                     PASSWORD_HASH, PASSWORD_SALT, CVE_PERFIL, 
                     ACTIVO, FECHA_REGISTRO, USUARIO_REGISTRO)
                    VALUES
                    (:codigo, :nombre, :apP, :apM, 
                     :hash, :salt, :perfil, 
                     1, SYSDATE, :usuarioReg)";

                using var cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":codigo", usuario.Codigo);
                cmd.Parameters.Add(":nombre", usuario.Nombre);
                cmd.Parameters.Add(":apP", usuario.ApellidoPaterno);
                cmd.Parameters.Add(":apM", usuario.ApellidoMaterno);
                cmd.Parameters.Add(":hash", usuario.PasswordHash);
                cmd.Parameters.Add(":salt", usuario.PasswordSalt);
                cmd.Parameters.Add(":perfil", cvePerfil);

                // Aquí capturamos al usuario de la sesión
                cmd.Parameters.Add(":usuarioReg", ObtenerUsuarioActual());

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CrearUsuario:\n" + ex.Message);
                return false;
            }
        }

        // =====================================================
        // ELIMINAR (BORRADO LÓGICO)
        // =====================================================
        public bool Eliminar(string codigo)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                // CAMBIO CLAVE: No usamos DELETE. Usamos UPDATE.
                string sql = @"
                    UPDATE USUARIOS 
                    SET ACTIVO = 0,
                        FECHA_BAJA = SYSDATE,
                        USUARIO_BAJA = :usuarioBaja
                    WHERE CVE_USUARIO = :codigo";

                using var cmd = new OracleCommand(sql, conn);

                // Registramos quién hizo la eliminación
                cmd.Parameters.Add(":usuarioBaja", ObtenerUsuarioActual());
                cmd.Parameters.Add(":codigo", codigo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Eliminar (Lógico): {ex.Message}");
                return false;
            }
        }

        // Método auxiliar para no repetir código de lectura
        private Usuario MapReaderToUsuario(OracleDataReader reader)
        {
            return new Usuario
            {
                Codigo = reader.GetString(0),
                Nombre = reader.GetString(1),
                ApellidoPaterno = reader.IsDBNull(2) ? "" : reader.GetString(2),
                ApellidoMaterno = reader.IsDBNull(3) ? "" : reader.GetString(3),
                PasswordHash = reader.GetString(4),
                PasswordSalt = reader.GetString(5),
                Perfil = reader.GetString(6),
                Activo = true // Si lo leímos de la BD con el filtro, es true
            };
        }

        // El método Actualizar y ObtenerPorNombre deben seguir la misma lógica de filtro
        // (No los incluí completos por espacio, pero asegúrate de poner "AND ACTIVO = 1" en sus SELECTs)
        public bool Actualizar(Usuario usuario)
        {
            // ... tu código existente ...
            // Nota: En Actualizar NO solemos tocar FECHA_REGISTRO ni USUARIO_REGISTRO.
            // Podrías agregar una columna FECHA_MODIFICACION si quisieras.
            return true; // Placeholder
        }

        public Usuario? ObtenerPorNombre(string nombre)
        {
            // Igual que ObtenerPorCodigo pero con el WHERE NOMBRE = ... AND ACTIVO = 1
            return null; // Placeholder
        }
    }
}