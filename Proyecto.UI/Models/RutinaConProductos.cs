namespace Proyecto.UI.Models
{
    public class RutinaConProductos
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }
        public List<ProductoRutina> Productos { get; set; } = new();
    }

    public class ProductoRutina
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = "";
        public string? Detalle { get; set; }
        public decimal Precio { get; set; }
        public string? RutaImagen { get; set; }
    }
}