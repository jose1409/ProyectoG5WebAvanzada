using System.Security.Cryptography;
using System.Text;

namespace Proyecto.UI.Utils
{
    public class Utilitarios : IUtilitarios
    {
        private readonly IConfiguration _configuration;

        public Utilitarios(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public byte[] ConvertImageToBytes(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray(); // esto puedes guardar en varbinary(MAX)
            }
        }
    }
}
