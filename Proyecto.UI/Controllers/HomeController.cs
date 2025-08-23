using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Repository.CategoriaRepository;

namespace Proyecto.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICategoriaRepository _categoriaRepository;

        public HomeController(ILogger<HomeController> logger, ICategoriaRepository categoriaRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            var categorias = _categoriaRepository.ObtenerTodasCategorias();
            ViewBag.Categorias = categorias;
            return View();
        }

        public IActionResult IndexAdmin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        public IActionResult Principal()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            {
                return RedirectToAction("Index", "Autenticacion");
            }

            return View();
        }
        public ActionResult Historia()
        {
            return View();
        }

    }
}
