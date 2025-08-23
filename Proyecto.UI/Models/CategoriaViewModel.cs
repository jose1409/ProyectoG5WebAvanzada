
namespace Proyecto.UI.Models
{
    public class CategoriaViewModel
    {
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}