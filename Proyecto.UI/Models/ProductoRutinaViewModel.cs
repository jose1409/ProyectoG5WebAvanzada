namespace Proyecto.UI.Models.ViewModels
{
    public class ProductoRutinaViewModel
    {
        public int id_producto { get; set; }
        public string descripcion { get; set; } = string.Empty;
        public string detalle { get; set; } = string.Empty;
        public double precio { get; set; }
        public string ruta_imagen { get; set; } = string.Empty;
    }
}