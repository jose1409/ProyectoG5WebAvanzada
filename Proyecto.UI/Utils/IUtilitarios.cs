namespace Proyecto.UI.Utils
{
    public interface IUtilitarios
    {
        string Encrypt(string texto);

        byte[] ConvertImageToBytes(IFormFile file);

    }
}
