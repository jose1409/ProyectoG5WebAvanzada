using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Repository.CategoriaRepository;
using Proyecto.UI.Repository.ProductoRepository;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AuthorizeSession(Rol = "Administrador")]
    public class ProductosAController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUtilitarios _utilitarios;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductosAController(IProductoRepository productoRepository, IUtilitarios utilitarios, ICategoriaRepository categoriaRepository)
        {
            _productoRepository = productoRepository;
            _utilitarios = utilitarios;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var resultado = _productoRepository.ObtenerTodos();
            ViewBag.Categorias = _categoriaRepository.ObtenerTodasCategorias();
            if (resultado != null)
            {
                return View(resultado);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult ActualizarProducto(Producto producto)
        {
            if (producto.Imagen64 != null)
            {
                // Nueva imagen subida: conviertes IFormFile a bytes
                producto.RutaImagen = _utilitarios.ConvertImageToBytes(producto.Imagen64);
            }
            else
            {
                // No hay archivo nuevo, decodificas el base64 para mantener la imagen existente
                producto.ImagenBase64 = producto.ImagenBase64!.Substring(producto.ImagenBase64.IndexOf(",") + 1);
                producto.RutaImagen = Convert.FromBase64String(producto.ImagenBase64!);
            }

            var resultado = _productoRepository.ActualizarProducto(producto);
            if (resultado >= 0)
            {
                return RedirectToAction("Index", "ProductosA");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult CrearProducto(Producto producto)
        {
            if (producto.Imagen64 != null)
            {
                // Nueva imagen subida: conviertes IFormFile a bytes
                producto.RutaImagen = _utilitarios.ConvertImageToBytes(producto.Imagen64);
            }
            var resultado = _productoRepository.CrearProducto(producto);
            if (resultado != null)
            {
                return RedirectToAction("Index", "ProductosA");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarProducto(int idProducto)
        {
            var resultado = _productoRepository.EliminarProducto(idProducto);
            if (resultado)
            {
                return RedirectToAction("Index", "ProductosA");
            }
            else
            {
                return View();
            }
        }
    }
}
