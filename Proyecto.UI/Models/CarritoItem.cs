// Proyecto.UI/Models/CarritoItem.cs
namespace Proyecto.UI.Models
{
    public class CarritoItem
    {
        public int? IdDetalle { get; set; }          // 👈 NUEVO (nullable)
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal Subtotal => Precio * Cantidad;
    }
}

