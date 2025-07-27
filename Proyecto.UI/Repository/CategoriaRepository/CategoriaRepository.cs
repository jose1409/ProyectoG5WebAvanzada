using System.Net.Http;
using System.Net.Http.Json;
using Proyecto.UI.Models;
using Proyecto.UI.Utils;
using static System.Net.WebRequestMethods;

namespace Proyecto.UI.Repository.CategoriaRepository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _http;
        private readonly IUtilitarios _utilitarios;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public CategoriaRepository(IConfiguration configuration, IHttpClientFactory http, IUtilitarios utilitarios, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _http = http;
            _utilitarios = utilitarios;
            _httpContextAccessor = httpContextAccessor;
        }


        public int ActualizarCategoria(Categoria categoria)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.PutAsJsonAsync("Categoria/EditarCategoria", categoria).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<int>>().Result;
                    return datos!.Contenido;
                } else
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<int>>().Result;
                    throw new Exception(datos!.Mensaje);
                }
            }
        }

        public Categoria CrearCategoria(Categoria categoria)
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.PostAsJsonAsync("Categoria/CrearCategoria", categoria).Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<Categoria>>().Result;
                    return datos?.Contenido!;
                }
                else
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse>().Result;
                    throw new Exception(datos!.Mensaje);
                }
            }
        }

        public bool EliminarCategoria(int idCategoria)
        {
            using (var http = _http.CreateClient()) 
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.DeleteAsync($"Categoria/EliminarCategoria/{idCategoria}").Result;
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

        public Task<List<Categoria>> ObtenerCategoriaPoNombred(string descripcion)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> ObtenerTodasCategorias()
        {
            using (var http = _http.CreateClient())
            {
                http.BaseAddress = new Uri(_configuration.GetSection("Start:ApiWeb").Value!);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + _httpContextAccessor!.HttpContext!.Session.GetString("JWT"));
                var resultado = http.GetAsync("Categoria/ObtenerTodos").Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var datos = resultado.Content.ReadFromJsonAsync<ApiResponse<List<Categoria>>>().Result;
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
