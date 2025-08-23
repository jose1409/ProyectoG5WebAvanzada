// API/Controllers/CarritoController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;                          // CarritoDto, CarritoItemDto, CarritoAgregarRequest
using API.Repository.CarritoRepository;    // ICarritoRepository

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoRepository _repo;
        public CarritoController(ICarritoRepository repo) => _repo = repo;

        // GET api/carrito/{idUsuario}
        [HttpGet("{idUsuario:int}")]
        public async Task<ActionResult<CarritoDto>> Obtener(int idUsuario)
        {
            if (idUsuario <= 0) return BadRequest("IdUsuario inválido.");
            var data = await _repo.ObtenerAsync(idUsuario);   // 👈 devuelve CarritoDto
            return Ok(data);
        }

        // POST api/carrito/items
        [HttpPost("items")]
        public async Task<ActionResult<object>> Agregar([FromBody] CarritoAgregarRequest req)
        {
            if (req is null || req.IdUsuario <= 0 || req.IdProducto <= 0)
                return BadRequest("Datos requeridos: IdUsuario, IdProducto, Cantidad.");
            if (req.Cantidad <= 0) req.Cantidad = 1;

            var idDetalle = await _repo.AgregarAsync(req.IdUsuario, req.IdProducto, req.Cantidad);
            return Ok(new { IdDetalle = idDetalle });
        }

        // PUT api/carrito/items/{idDetalle}?cantidad=#
        [HttpPut("items/{idDetalle:int}")]
        public async Task<IActionResult> CambiarCantidad(int idDetalle, [FromQuery] int cantidad)
        {
            if (idDetalle <= 0) return BadRequest("IdDetalle inválido.");
            if (cantidad < 0) return BadRequest("Cantidad no puede ser negativa.");

            var afectados = await _repo.ActualizarCantidadAsync(idDetalle, cantidad);
            if (afectados == 0) return NotFound("Detalle no encontrado.");
            return NoContent();
        }

        // DELETE api/carrito/items/{idDetalle}
        [HttpDelete("items/{idDetalle:int}")]
        public async Task<IActionResult> Eliminar(int idDetalle)
        {
            if (idDetalle <= 0) return BadRequest("IdDetalle inválido.");

            var afectados = await _repo.EliminarItemAsync(idDetalle);
            if (afectados == 0) return NotFound("Detalle no encontrado.");
            return NoContent();
        }

        // DELETE api/carrito/{idUsuario}
        [HttpDelete("{idUsuario:int}")]
        public async Task<IActionResult> Vaciar(int idUsuario)
        {
            if (idUsuario <= 0) return BadRequest("IdUsuario inválido.");
            await _repo.VaciarAsync(idUsuario);
            return NoContent();
        }

        // POST api/carrito/{idUsuario}/checkout
        [HttpPost("{idUsuario:int}/checkout")]
        public async Task<ActionResult<object>> Checkout(int idUsuario)
        {
            if (idUsuario <= 0) return BadRequest("IdUsuario inválido.");
            try
            {
                var idPedido = await _repo.CheckoutAsync(idUsuario);
                return Ok(new { IdPedido = idPedido });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
