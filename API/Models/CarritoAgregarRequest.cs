namespace API.Models
{
    public class CarritoAgregarRequest
    {
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; } = 1;
    }
}
