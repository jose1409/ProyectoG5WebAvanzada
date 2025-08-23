namespace API.Models
{
    public class CarritoItemDto
    {
        public int? IdDetalle { get; set; }          // puede venir null
        public int ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad; // opcional pero útil
    }
}


