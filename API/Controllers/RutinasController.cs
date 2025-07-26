using API.Models;
using API.Repository.RutinaRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutinasController : ControllerBase
    {
        private readonly IRutinaRepository _repo;

        public RutinasController(IRutinaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _repo.ObtenerRutinas();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var rutina = _repo.ObtenerRutinaPorId(id);
            if (rutina == null)
                return NotFound();

            return Ok(rutina);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rutina rutina)
        {
            var id = _repo.InsertarRutina(rutina);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Rutina rutina)
        {
            rutina.IdRutina = id;
            _repo.ActualizarRutina(rutina);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.EliminarRutina(id);
            return Ok();
        }
    }
}