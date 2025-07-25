using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var idUsuario = _httpContextAccessor.HttpContext?.Session.GetString("IdUsuario");

            if (string.IsNullOrEmpty(idUsuario))
            {
                // Si no hay sesión, redirige a login
                return RedirectToAction("Index", "Autenticacion");
            }

            ViewBag.IdUsuario = idUsuario;
            return View();
        }
    }
}
