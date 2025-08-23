using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Repository.CategoriaRepository;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [AuthorizeSession(Rol = "Administrador")]
    //[Authorize]
    public class CategoriaAController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUtilitarios _utilitarios;

        public CategoriaAController(ICategoriaRepository categoriaRepository, IUtilitarios utilitarios)
        {
            _categoriaRepository = categoriaRepository;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var resultado = _categoriaRepository.ObtenerTodasCategorias();
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
        public IActionResult ActualizarCategoria(Categoria categoria)
        {
            if (categoria.Imagen64 != null)
            {
                // Nueva imagen subida: conviertes IFormFile a bytes
                categoria.Imagen = _utilitarios.ConvertImageToBytes(categoria.Imagen64);
            }
            else
            {
                // No hay archivo nuevo, decodificas el base64 para mantener la imagen existente
                categoria.ImagenBase64 = categoria.ImagenBase64!.Substring(categoria.ImagenBase64.IndexOf(",") + 1);
                categoria.Imagen = Convert.FromBase64String(categoria.ImagenBase64!);
            }

            var resultado = _categoriaRepository.ActualizarCategoria(categoria);
            if (resultado >= 0)
            {
                return RedirectToAction("Index", "CategoriaA");
            } else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult CrearCategoria(Categoria categoria)
        {
            if (categoria.Imagen64 != null)
            {
                // Nueva imagen subida: conviertes IFormFile a bytes
                categoria.Imagen = _utilitarios.ConvertImageToBytes(categoria.Imagen64);
            }
            var resultado = _categoriaRepository.CrearCategoria(categoria);
            if (resultado != null)
            {
                return RedirectToAction("Index", "CategoriaA");
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EliminarCategoria (int idCategoria)
        {
            var resultado = _categoriaRepository.EliminarCategoria(idCategoria);
            if(resultado)
            {
                TempData["Mensaje"] = "La categoría se eliminó correctamente.";
                return RedirectToAction("Index", "CategoriaA");
            } else
            {
                TempData["Error"] = "No se puede eliminar la categoría porque tiene productos asociados.";
                return RedirectToAction("Index", "CategoriaA");
            }
        }
    }
}
