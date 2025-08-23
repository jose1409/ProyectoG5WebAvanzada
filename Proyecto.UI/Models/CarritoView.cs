using System.Collections.Generic;
using System.Linq;

namespace Proyecto.UI.Models
{
    public class CarritoView
    {
        // ✅ SOLO esta propiedad. Nada de fields ni duplicados.
        public List<CarritoItem> Items { get; set; } = new();

        // ✅ Calculadas (así no se desincronizan)
        public decimal Total => Items.Sum(x => x.Subtotal);
        public int Count => Items.Sum(x => x.Cantidad);
    }
}

