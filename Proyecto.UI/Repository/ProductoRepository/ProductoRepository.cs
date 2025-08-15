
using Proyecto.UI.Models;
using Proyecto.UI.Utils;

namespace Proyecto.UI.Repository.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        private readonly IUtilitarios _utilitarios;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductoRepository(IConfiguration configuration, IHttpClientFactory http, IUtilitarios utilitarios, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _http = http;
            _utilitarios = utilitarios;
            _httpContextAccessor = httpContextAccessor;
        }


        public int ActualizarProducto(Producto data)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("Producto/EditarProducto", data).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<int>>().Result;
                    return datos!.Contenido;
                }
                else
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<int>>().Result;
                    throw new Exception(datos!.Mensaje);
                }
            }
        }

        public Producto CrearProducto(Producto data)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("Producto/CrearProducto", data).Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<Producto>>().Result;
                    return datos?.Contenido!;
                }
                else
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    throw new Exception(datos!.Mensaje);
                }
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.DeleteAsync($"Producto/EliminarProducto/{idProducto}").Result;
                if (resultado.IsSuccessStatusCode)
                {
                    return resultado.IsSuccessStatusCode;
                }
                else
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    throw new Exception(datos!.Mensaje);
                }
            }
        }

        public List<Producto> ObtenerTodos()
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.GetAsync("Producto/ObtenerTodos").Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<List<Producto>>>().Result;
                    return datos?.Contenido!;
                }
                else
                {
                    var respuesta = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    throw new Exception(respuesta!.Mensaje);
                }
            }
        }
    }
}
