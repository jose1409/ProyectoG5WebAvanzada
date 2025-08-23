namespace API.Models
{
    public class Rutina
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public List<int>? IdsProductos { get; set; }
    }
}