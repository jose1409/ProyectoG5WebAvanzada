using Proyecto.UI.Models;
using Proyecto.UI.Services.Interfaces;
using System.Net.Http.Json;

namespace Proyecto.UI.Services.Implementaciones
{
    public class RutinaService : IRutinaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RutinaService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<Rutina>> ObtenerRutinasAsync()
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);
            var response = await http.GetFromJsonAsync<List<Rutina>>("api/Rutina");
            return response ?? new List<Rutina>();
        }

        public async Task<Rutina> ObtenerRutinaPorIdAsync(int id)
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);
            return await http.GetFromJsonAsync<Rutina>($"api/Rutina/{id}");
        }

        public async Task<bool> InsertarRutinaAsync(Rutina rutina)
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);
            var response = await http.PostAsJsonAsync("api/Rutina", rutina);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarRutinaAsync(Rutina rutina)
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);
            var response = await http.PutAsJsonAsync($"api/Rutina/{rutina.IdRutina}", rutina);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarRutinaAsync(int id)
        {
            using var http = _httpClientFactory.CreateClient();
            http.BaseAddress = new Uri(_configuration["Start:ApiWeb"]);
            var response = await http.DeleteAsync($"api/Rutina/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}