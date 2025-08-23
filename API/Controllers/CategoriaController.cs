using API.Models;
using API.Repository.CategoriaRepository;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUtilitarios _utilitarios;

        public CategoriaController(ICategoriaRepository categoriaRepository, IUtilitarios utilitarios)
        {
            _categoriaRepository = categoriaRepository;
            _utilitarios = utilitarios;
        }

        [HttpGet]
        [Route("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            List<Categoria> categorias = _categoriaRepository.ObtenerTodos();
            return Ok(_utilitarios.RespuestaCorrecta(categorias));
        }

        [HttpPut]
        [Route("EditarCategoria")]
        public IActionResult EditarCategoria(Categoria categoria)
        {
            int resultado = _categoriaRepository.Actualizar(categoria);
            return Ok(_utilitarios.RespuestaCorrecta(resultado));
        }

        [HttpPost]
        [Route("CrearCategoria")]
        public IActionResult CrearCategoria(Categoria categoria)
        {
            Categoria resultado = _categoriaRepository.Crear(categoria);
            return Ok(_utilitarios.RespuestaCorrecta(resultado));
        }

        [HttpDelete("EliminarCategoria/{id}")]
        public IActionResult EliminarCategoria(int id)
        {
            bool resultado = _categoriaRepository.Eliminar(id);
            if (resultado)
            {
                return Ok(_utilitarios.RespuestaCorrecta("Categoría eliminada correctamente."));
            }
            else
            {
                return Conflict(_utilitarios.RespuestaIncorrecta("No se puede eliminar la categoria ya que posee productos vinculados"));
            }
        }
        
        [HttpGet("ObtenerPorId/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                var categoria = _categoriaRepository.ObtenerPorId(id);
                if (categoria == null)
                {
                    return NotFound(_utilitarios.RespuestaIncorrecta("Categoría no encontrada."));
                }

                return Ok(_utilitarios.RespuestaCorrecta(categoria));
            }
            catch (Exception ex)
            {
                return StatusCode(500, _utilitarios.RespuestaIncorrecta("Error en el servidor: " + ex.Message));
            }
        }
    }
}
