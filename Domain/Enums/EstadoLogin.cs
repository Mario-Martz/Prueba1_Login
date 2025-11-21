namespace Prueba1_Login.Domain.Enums
{
    public enum EstadoLogin
    {
        Exitoso,
        Vacios,
        UsuarioIncorrecto,
        ContraseñaIncorrecta,
        ErrorConexion  // ✅ NUEVO: Para errores de conexión a BD
    }
}