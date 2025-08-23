using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class RutinaConProductos
    {
        public int IdRutina { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }

        public byte[]? Imagen { get; set; } 

        [NotMapped]
        public string RutaImagen { get; set; } = ""; 

        public List<ProductoRutina> Productos { get; set; } = new();
    }

    public class ProductoRutina
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = "";
        public string Detalle { get; set; } = "";
        public decimal Precio { get; set; }
        public int Existencias { get; set; }

        public byte[]? ImagenBinaria { get; set; }
        public string RutaImagen { get; set; } = "";
    }
}