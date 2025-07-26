using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Utils
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public Utilitarios (IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string Encrypt(string texto)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveSegura").Value!);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(texto);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            var remitente = _configuration.GetSection("SMTP:Remitente").Value;
            var contrasenna = _configuration.GetSection("SMTP:Contrasenna").Value;

            if (!string.IsNullOrEmpty(remitente) && !string.IsNullOrEmpty(contrasenna))
            {
                var mensaje = new MailMessage(remitente, destinatario, asunto, cuerpo);
                mensaje.IsBodyHtml = true;

                var smtp = new SmtpClient("smtp.office365.com", 587)
                {
                    Credentials = new NetworkCredential(remitente, contrasenna),
                    EnableSsl = true
                };

                smtp.Send(mensaje);
            }
        }

        public string GenerarContrasena()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var resultado = new System.Text.StringBuilder(8);

            for (int i = 0; i < 8; i++)
            {
                int indice = random.Next(caracteres.Length);
                resultado.Append(caracteres[indice]);
            }

            return resultado.ToString();
        }

        public string GenerarToken(long IdUsuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Start:LlaveSegura").Value!);

            var claims = new[]
            {
                new Claim("IdUsuario", IdUsuario.ToString()),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public ApiResponse RespuestaCorrecta(object contenido)
        {
            return new ApiResponse
            {
                Codigo = 0,
                Mensaje = "Operación exitosa",
                Contenido = contenido
            };
        }

        public ApiResponse RespuestaIncorrecta(string mensaje)
        {
            return new ApiResponse
            {
                Codigo = 99,
                Mensaje = mensaje,
                Contenido = null
            };
        }

        public void SMTPCorreo(Autenticacion autenticacion, string contrasenna)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Emails", "Correos.html");
            var html = File.ReadAllText(path);

            html = html.Replace("@@NombreUsuario", autenticacion.Nombre);
            html = html.Replace("@@Contrasenna", contrasenna);

            EnviarCorreo(autenticacion.CorreoElectronico!, "Acceso al Sistema", html);
        }
    }
}
