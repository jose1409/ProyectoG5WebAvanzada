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
        //[Authorize]
        public IActionResult ObtenerTodos()
        {
            List<Categoria> categorias = _categoriaRepository.ObtenerTodos();
            return Ok(_utilitarios.RespuestaCorrecta(categorias));
        }
    }
}
