using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.ViewModels;
using System.Net.Http.Json;

namespace Proyecto.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly HttpClient _httpClient;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _httpClient.GetFromJsonAsync<AboutViewModel>("api/about");
            return View(data);
        }
    }
}

