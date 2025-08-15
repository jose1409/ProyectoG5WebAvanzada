namespace API.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public double Precio { get; set; }
        public int Existencias { get; set; }
        public byte[]? RutaImagen { get; set; }
        public bool Activo { get; set; }
    }
}
