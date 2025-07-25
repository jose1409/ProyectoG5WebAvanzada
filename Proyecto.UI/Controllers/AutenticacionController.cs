using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    public class AutenticacionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        private readonly IUtilitarios _utilitarios;

        public AutenticacionController(IConfiguration configuration, IHttpClientFactory http, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _http = http;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            {
                return RedirectToAction("Principal", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(Autenticacion autenticacion)
        {
            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna!);

            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                var resultado = http.PostAsJsonAsync("Autenticacion/Login", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<Autenticacion>>().Result;

                    HttpContext.Session.SetString("IdUsuario", datos?.Contenido?.IdUsuario.ToString()!);
                    HttpContext.Session.SetString("Nombre", datos?.Contenido?.Nombre!);
                    HttpContext.Session.SetString("JWT", datos?.Contenido?.Token!);

                    return RedirectToAction("Principal", "Home");  //Configurar Ventana principal
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    ViewBag.Mensaje = respuesta!.Mensaje;
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult Registro()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            {
                return RedirectToAction("Principal", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Registro(Autenticacion autenticacion)
        {
            if (!ModelState.IsValid)
            {
                return View(autenticacion);
            }

            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna!);

            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                var resultado = http.PostAsJsonAsync("Autenticacion/Register", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Autenticacion");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    ViewBag.Mensaje = respuesta?.Mensaje ?? "Ocurrió un error al registrarse.";
                    return View(autenticacion);
                }
            }
        }

        [HttpGet]
        public IActionResult RecoverAccess()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            {
                return RedirectToAction("Principal", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult RecoverAccess(Autenticacion autenticacion)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                var resultado = http.PostAsJsonAsync("Autenticacion/RecoverAccess", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha enviado un correo con la nueva contraseña.";
                    ViewBag.TipoMensaje = "success";
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo recuperar el acceso. Verifique su correo.";
                    ViewBag.TipoMensaje = "danger";
                }

                return View();
            }
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Autenticacion");
        }

    }
}
