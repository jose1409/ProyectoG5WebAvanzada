using API.Models;

namespace API.Utils
{
    public interface IUtilitarios
    {
        ApiResponse RespuestaCorrecta(object contenido);

        ApiResponse RespuestaIncorrecta(string mensaje);

        string GenerarToken(long IdUsuario, string Rol);

        string Encrypt(string texto);

        void EnviarCorreo(string destinatario, string asunto, string cuerpo);

        void SMTPCorreo(Autenticacion autenticacion, String contrasenna);

        string GenerarContrasena();
    }
}
