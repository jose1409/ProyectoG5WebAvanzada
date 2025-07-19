using API.Models;
using API.Utils;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        private readonly IUtilitarios _utilitarios;
        public UsuarioController(IConfiguration configuration, IHostEnvironment environment, IUtilitarios utilitarios)
        {
            _configuration = configuration;
            _environment = environment;
            _utilitarios = utilitarios;
        }

        //[Authorize]
        [HttpGet]
        [Route("getUserProfileData")]
        public IActionResult getUserProfileData(long IdUsuario)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.QueryFirstOrDefault<Autenticacion>("GetUserProfileData", new
                {
                    IdUsuario
                });

                if (resultado != null)
                {
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                }
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("No se encontró información"));
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("updateUserProfileData")]
        public IActionResult updateUserProfileData(Autenticacion autenticacion)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Execute("UpdateUserProfileData", new
                {
                    autenticacion.IdUsuario,
                    autenticacion.Cedula,
                    autenticacion.Nombre,
                    autenticacion.Apellidos,
                    autenticacion.CorreoElectronico,
                    autenticacion.Telefono,
                    autenticacion.Fotografia
                });

                if (resultado > 0)
                    return Ok(_utilitarios.RespuestaCorrecta(autenticacion));
                else
                    return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue actualizada correctamente"));
            }
        }
    }
}