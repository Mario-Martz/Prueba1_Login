namespace Prueba1_Login.Domain.Entities
{
    public class Usuario
    {
        // ===================================
        // Campos de Negocio
        // ===================================
        public string Codigo { get; set; } // Mapea a CVE_USUARIO (PK)
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Perfil { get; set; } // Descripción del perfil (Texto)

        // ===================================
        // Campos de Seguridad (Reemplazan a 'Contrasena')
        // ===================================
        public string PasswordHash { get; set; } // Contraseña hasheada
        public string PasswordSalt { get; set; } // Salt para el hash
    }
}