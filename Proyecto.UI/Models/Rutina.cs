namespace Proyecto.UI.Models
{
    public class Rutina
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

        public List<ViewModels.ProductoRutinaViewModel> Productos { get; set; } = new();
    }
}