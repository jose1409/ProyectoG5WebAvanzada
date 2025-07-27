namespace Proyecto.UI.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public bool Activo { get; set; }


        public IFormFile? Imagen64 { get; set; }
        public string? ImagenBase64 { get; set; }
    }
}
