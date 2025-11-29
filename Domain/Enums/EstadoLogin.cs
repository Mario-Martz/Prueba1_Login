namespace Prueba1_Login.Domain.Enums
{
    public enum EstadoLogin
    {
        Exitoso,
        Vacios,
        UsuarioIncorrecto,
        ContraseñaIncorrecta,
        ErrorConexion  //Para errores de conexión a BD
    }
}