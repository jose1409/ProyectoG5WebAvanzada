using System.Collections.Generic;

namespace API.Models
{
    public class CarritoDto
    {
        public List<CarritoItemDto> Items { get; set; } = new();
        // Si quieres, puedes agregar calculadas:
        // public decimal Total => Items.Sum(i => i.Subtotal);
        // public int Count => Items.Sum(i => i.Cantidad);
    }
}


