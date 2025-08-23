// Proyecto.UI/Controllers/CarritoController.cs
using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Proyecto.UI.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IHttpClientFactory _factory;
        public CarritoController(IHttpClientFactory factory) => _factory = factory;

        private int IdUsuario => 1;

        private HttpClient Api() => _factory.CreateClient("API");

        private async Task<CarritoView> GetCartAsync()
        {
            var carrito = await Api().GetFromJsonAsync<CarritoView>($"api/carrito/{IdUsuario}");
            return carrito ?? new CarritoView();
        }

        private object BuildSummary(CarritoView cart, int? idDetalle = null)
        {
            decimal? sub = null;
            if (idDetalle.HasValue)
            {
                var it = cart.Items?.FirstOrDefault(x => x.IdDetalle.HasValue && x.IdDetalle.Value == idDetalle.Value);
                if (it != null) sub = it.Subtotal;
            }
            var total = cart.Items?.Sum(i => i.Subtotal) ?? 0m;
            var count = cart.Items?.Sum(i => i.Cantidad) ?? 0;
            return new { ok = true, total, count, subtotal = sub };
        }

        private async Task<int?> ResolveDetalleIdAsync(int idFromView)
        {
            var cart = await GetCartAsync();
            var byDetalle = cart.Items?.FirstOrDefault(x => x.IdDetalle.HasValue && x.IdDetalle.Value == idFromView);
            if (byDetalle != null) return byDetalle.IdDetalle;
            var byProd = cart.Items?.FirstOrDefault(x => x.ProductoId == idFromView);
            return byProd?.IdDetalle;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carrito = await GetCartAsync();
            return View(carrito);
        }

        [HttpGet]
        public async Task<IActionResult> Resumen()
        {
            var carrito = await GetCartAsync();
            if (carrito?.Items == null || !carrito.Items.Any())
            {
                TempData["Error"] = "Tu carrito está vacío.";
                return RedirectToAction(nameof(Index));
            }
            return View(carrito);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int idProducto, int cantidad = 1)
        {
            await Api().PostAsJsonAsync("api/carrito/items",
                new CarritoAgregarRequest { IdUsuario = IdUsuario, IdProducto = idProducto, Cantidad = cantidad });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarCantidad(int idDetalle, int cantidad)
        {
            var resp = await Api().PutAsync($"api/carrito/items/{idDetalle}?cantidad={cantidad}", null);
            resp.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int idDetalle)
        {
            var resp = await Api().DeleteAsync($"api/carrito/items/{idDetalle}");
            resp.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vaciar()
        {
            var resp = await Api().DeleteAsync($"api/carrito/{IdUsuario}");
            resp.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        // ✅ MODIFICADO: redirige a Gracias con el IdPedido
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var resp = await Api().PostAsync($"api/carrito/{IdUsuario}/checkout", null);
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = await resp.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Index));
            }

            var json = await resp.Content.ReadFromJsonAsync<dynamic>();
            var pedidoId = json?.IdPedido?.ToString() ?? "";

            return RedirectToAction(nameof(Gracias), new { id = pedidoId });
        }

        // ✅ NUEVO: página de agradecimiento
        [HttpGet]
        public IActionResult Gracias(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction(nameof(Index));
            return View(model: id); // Views/Carrito/Gracias.cshtml recibirá el IdPedido como string
        }

        [HttpGet]
        public async Task<IActionResult> Contador()
        {
            var carrito = await GetCartAsync();
            var cantidad = carrito?.Items?.Sum(i => i.Cantidad) ?? 0;
            var total = carrito?.Items?.Sum(i => i.Subtotal) ?? 0m;
            return Json(new { cantidad, total });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add(int id, int qty = 1)
        {
            await Api().PostAsJsonAsync("api/carrito/items",
                new CarritoAgregarRequest { IdUsuario = IdUsuario, IdProducto = id, Cantidad = qty });

            var cart = await GetCartAsync();
            return Json(BuildSummary(cart));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangeQty(int id, int qty)
        {
            var idDetalle = await ResolveDetalleIdAsync(id);
            if (idDetalle is null)
                return BadRequest(new { ok = false, message = "No se pudo identificar el ítem del carrito." });

            var resp = await Api().PutAsync($"api/carrito/items/{idDetalle}?cantidad={qty}", null);
            if (!resp.IsSuccessStatusCode) return BadRequest(new { ok = false });

            var cart = await GetCartAsync();
            return Json(BuildSummary(cart, idDetalle: idDetalle));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var idDetalle = await ResolveDetalleIdAsync(id);
            if (idDetalle is null)
                return BadRequest(new { ok = false, message = "No se pudo identificar el ítem del carrito." });

            var resp = await Api().DeleteAsync($"api/carrito/items/{idDetalle}");
            if (!resp.IsSuccessStatusCode) return BadRequest(new { ok = false });

            var cart = await GetCartAsync();
            return Json(BuildSummary(cart));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Clear()
        {
            var resp = await Api().DeleteAsync($"api/carrito/{IdUsuario}");
            if (!resp.IsSuccessStatusCode) return BadRequest(new { ok = false });

            var cart = await GetCartAsync();
            return Json(BuildSummary(cart));
        }

        [HttpGet]
        public async Task<IActionResult> Badge()
        {
            var cart = await GetCartAsync();
            var total = cart.Items?.Sum(i => i.Subtotal) ?? 0m;
            var count = cart.Items?.Sum(i => i.Cantidad) ?? 0;
            return Json(new { count, total });
        }
    }
}






