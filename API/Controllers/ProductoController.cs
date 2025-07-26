using API.Models;
using API.Repository.ProductoRepository;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IUtilitarios _utilitarios;
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IUtilitarios utilitarios, IProductoRepository productoRepository)
        {
            _utilitarios = utilitarios;
            _productoRepository = productoRepository;
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        //[Authorize]
        public IActionResult ObtenerTodos()
        {
            List<Producto> resultado = _productoRepository.ObtenerTodos();
            return Ok(_utilitarios.RespuestaCorrecta(resultado));
        }

        [HttpPost]
        [Route("ObtenerTodosFiltradoXCategoria")]
        //[Authorize]
        public IActionResult ObtenerTodosFiltradoXCategoria([FromBody] Categoria categoria)
        {
            List<Producto> resultado = _productoRepository.ObtenerTodosFiltradoXCategoria(categoria.IdCategoria);
            return Ok(_utilitarios.RespuestaCorrecta(resultado));
        }
    }
}
