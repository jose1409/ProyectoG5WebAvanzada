using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Repository.ProductoRepository;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUtilitarios _utilitarios;

        public ProductosController(IProductoRepository productoRepository, IUtilitarios utilitarios)
        {
            _productoRepository = productoRepository;
            _utilitarios = utilitarios;
        }

        public IActionResult Catalogo()
        {
            var productos = _productoRepository.ObtenerTodos();
            return View(productos);
        }
    }
}
