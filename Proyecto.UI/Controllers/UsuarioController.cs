using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _configuration;
        private readonly IUtilitarios _utilitarios;

        public UsuarioController(IHttpClientFactory http, IConfiguration configuration, IUtilitarios utilitarios)
        {
            _http = http;
            _configuration = configuration;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        public IActionResult Index()
        {
            using (var http = _http.CreateClient())
            {
                var IdUsuario = HttpContext.Session.GetString("IdUsuario");
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);

                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.GetAsync("/Usuario/getUserProfileData?IdUsuario=" + IdUsuario).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<Autenticacion>>().Result;
                    return View(datos!.Contenido);
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    ViewBag.Mensaje = respuesta!.Mensaje;
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult Index(Autenticacion autenticacion)
        {
            //Si no se selecciona una nueva imagen entonces devolvera la misma que ya existe
            if (autenticacion.FotografiaFile != null)
            {
                //Convertir PNG to Bytes
                autenticacion.Fotografia = _utilitarios.ConvertImageToBytes(autenticacion.FotografiaFile);
            }

            using (var http = _http.CreateClient())
            {
                var IdUsuario = HttpContext.Session.GetString("IdUsuario");
                autenticacion.IdUsuario = int.Parse(IdUsuario!);

                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("Usuario/updateUserProfileData", autenticacion).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("Nombre", autenticacion?.Nombre!);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    ViewBag.Mensaje = respuesta!.Mensaje;
                    return View();
                }
            }
        }
    }
}
