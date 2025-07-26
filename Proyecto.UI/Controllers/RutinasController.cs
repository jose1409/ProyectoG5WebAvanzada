using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Proyecto.UI.Controllers
{
    public class RutinasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RutinasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);

            var response = await client.GetAsync("api/rutinas");

            if (response.IsSuccessStatusCode)
            {
                var rutinas = await response.Content.ReadFromJsonAsync<List<Rutina>>();
                return View(rutinas);
            }

            ViewBag.Mensaje = "No se pudieron cargar las rutinas.";
            return View(new List<Rutina>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);

            var response = await client.GetAsync($"api/rutinas/{id}");

            if (response.IsSuccessStatusCode)
            {
                var rutina = await response.Content.ReadFromJsonAsync<Rutina>();
                return View(rutina);
            }

            return NotFound();
        }
    }
}