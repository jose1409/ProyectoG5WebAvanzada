using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto.UI.Controllers
{
    public class ProductosController : Controller
    {
        public IActionResult Catalogo()
        {
            var productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Labial Mate", Descripcion = "Color duradero y acabado profesional.", Precio = 8500, ImagenUrl = "https://pimg.amr.marykaycdn.com/HeroZoom/10004/10114285-Rosa%20Fresco.jpg" },
            new Producto { Id = 2, Nombre = "Crema Hidratante", Descripcion = "Hidratación intensa por 24 horas.", Precio = 12000, ImagenUrl = "https://farmacityar.vtexassets.com/arquivos/ids/266453-800-auto?v=638798938854670000&width=800&height=auto&aspect=true" },
            new Producto { Id = 3, Nombre = "Sombras de Ojos", Descripcion = "Paleta con 12 tonos vibrantes.", Precio = 10500, ImagenUrl = "https://bebeautycostarica.com/cdn/shop/files/ScreenShot2023-04-18at5.53.26PMnudex.webp?v=1701050478" },
            new Producto { Id = 4, Nombre = "Mascarilla Facial", Descripcion = "Purifica y suaviza tu piel al instante.", Precio = 6900, ImagenUrl = "https://pecosacr.com/wp-content/uploads/2025/02/299806-430x430.jpg" },
            new Producto { Id = 5, Nombre = "Base Líquida", Descripcion = "Cobertura perfecta para todo tipo de piel.", Precio = 15000, ImagenUrl = "https://www.shopperscr.com/822-large_default/base-liquida-2en1-tan-mlmpcf09.jpg" },
            new Producto { Id = 6, Nombre = "Rubor Rosado", Descripcion = "Toque natural de color en tus mejillas.", Precio = 7800, ImagenUrl = "https://adrissabeauty.com/cdn/shop/files/sku_23005151_color_perfect-pink-cool-pink_01_3000x.progressive-removebg-preview.png?v=1689773825" },
            new Producto { Id = 7, Nombre = "Delineador Líquido", Descripcion = "Precisión y color intenso.", Precio = 5500, ImagenUrl = "https://siman.vtexassets.com/arquivos/ids/372611/309975045024.jpg?v=637250986185030000" },
            new Producto { Id = 8, Nombre = "Serum Vitamina C", Descripcion = "Ilumina y mejora el tono de la piel.", Precio = 18000, ImagenUrl = "https://static.sweetcare.com/img/prd/488/v-638392922403618886/avene-020078an_01.webp" },
            
        };

            return View(productos);
        }
    }
}
