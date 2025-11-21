using Oracle.ManagedDataAccess.Client;
using Prueba1_Login.Domain.Entities;
using Prueba1_Login.Domain.Interfaces;

namespace Prueba1_Login.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // =====================================================
        // OBTENER POR CÓDIGO
        // =====================================================
        public Usuario? ObtenerPorCodigo(string codigo)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"
                    SELECT 
                        U.CVE_USUARIO, 
                        U.NOMBRE, 
                        U.APELLIDO_PATERNO, 
                        U.APELLIDO_MATERNO,
                        U.PASSWORD_HASH,
                        U.PASSWORD_SALT,
                        P.DESCRIPCION
                    FROM USUARIOS U
                    JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL
                    WHERE U.CVE_USUARIO = :cve";

                using var cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":cve", codigo);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Usuario
                    {
                        Codigo = reader.GetString(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        PasswordHash = reader.GetString(4),
                        PasswordSalt = reader.GetString(5),
                        Perfil = reader.GetString(6)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerPorCodigo: {ex.Message}");
            }

            return null;
        }

        // =====================================================
        // OBTENER POR NOMBRE
        // =====================================================
        public Usuario? ObtenerPorNombre(string nombre)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"
                    SELECT 
                        U.CVE_USUARIO, 
                        U.NOMBRE, 
                        U.APELLIDO_PATERNO, 
                        U.APELLIDO_MATERNO,
                        U.PASSWORD_HASH,
                        U.PASSWORD_SALT,
                        P.DESCRIPCION
                    FROM USUARIOS U
                    JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL
                    WHERE UPPER(U.NOMBRE) = :nombre";

                using var cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":nombre", nombre.ToUpper());

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Usuario
                    {
                        Codigo = reader.GetString(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        PasswordHash = reader.GetString(4),
                        PasswordSalt = reader.GetString(5),
                        Perfil = reader.GetString(6)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerPorNombre: {ex.Message}");
            }

            return null;
        }

        // =====================================================
        // OBTENER TODOS (NO TRAE HASH)
        // =====================================================
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new();

            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"
                    SELECT 
                        U.CVE_USUARIO, 
                        U.NOMBRE, 
                        U.APELLIDO_PATERNO, 
                        U.APELLIDO_MATERNO,
                        P.DESCRIPCION
                    FROM USUARIOS U
                    JOIN PERFILES P ON U.CVE_PERFIL = P.CVE_PERFIL";

                using var cmd = new OracleCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        Codigo = reader.GetString(0),
                        Nombre = reader.GetString(1),
                        ApellidoPaterno = reader.GetString(2),
                        ApellidoMaterno = reader.GetString(3),
                        Perfil = reader.GetString(4)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ObtenerTodos: {ex.Message}");
            }

            return lista;
        }

        // =====================================================
        // CREAR
        // =====================================================
        public bool Crear(Usuario usuario)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"
                    INSERT INTO USUARIOS
                    (CVE_USUARIO, NOMBRE, APELLIDO_PATERNO, APELLIDO_MATERNO, 
                     PASSWORD_HASH, PASSWORD_SALT, CVE_PERFIL)
                    VALUES
                    (:codigo, :nombre, :apP, :apM, :hash, :salt,
                        (SELECT CVE_PERFIL FROM PERFILES WHERE DESCRIPCION = :perfil))";

                using var cmd = new OracleCommand(sql, conn);

                cmd.Parameters.Add(":codigo", usuario.Codigo);
                cmd.Parameters.Add(":nombre", usuario.Nombre);
                cmd.Parameters.Add(":apP", usuario.ApellidoPaterno);
                cmd.Parameters.Add(":apM", usuario.ApellidoMaterno);
                cmd.Parameters.Add(":hash", usuario.PasswordHash);
                cmd.Parameters.Add(":salt", usuario.PasswordSalt);
                cmd.Parameters.Add(":perfil", usuario.Perfil);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error CrearUsuario: {ex.Message}");
                return false;
            }
        }

        // =====================================================
        // ACTUALIZAR
        // =====================================================
        public bool Actualizar(Usuario usuario)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"
                    UPDATE USUARIOS
                    SET NOMBRE = :nombre,
                        APELLIDO_PATERNO = :apP,
                        APELLIDO_MATERNO = :apM,
                        PASSWORD_HASH = :hash,
                        PASSWORD_SALT = :salt,
                        CVE_PERFIL = (SELECT CVE_PERFIL FROM PERFILES WHERE DESCRIPCION = :perfil)
                    WHERE CVE_USUARIO = :codigo";

                using var cmd = new OracleCommand(sql, conn);

                cmd.Parameters.Add(":nombre", usuario.Nombre);
                cmd.Parameters.Add(":apP", usuario.ApellidoPaterno);
                cmd.Parameters.Add(":apM", usuario.ApellidoMaterno);
                cmd.Parameters.Add(":hash", usuario.PasswordHash);
                cmd.Parameters.Add(":salt", usuario.PasswordSalt);
                cmd.Parameters.Add(":perfil", usuario.Perfil);
                cmd.Parameters.Add(":codigo", usuario.Codigo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Actualizar: {ex.Message}");
                return false;
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public bool Eliminar(string codigo)
        {
            try
            {
                using var conn = DatabaseConnection.GetConnection();
                conn.Open();

                string sql = @"DELETE FROM USUARIOS WHERE CVE_USUARIO = :codigo";

                using var cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":codigo", codigo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Eliminar: {ex.Message}");
                return false;
            }
        }
    }
}