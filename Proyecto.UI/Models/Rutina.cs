namespace Proyecto.UI.Models
{
    public class Rutina
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

        public List<RutinaConProductos> Productos { get; set; } = new();
    }
}