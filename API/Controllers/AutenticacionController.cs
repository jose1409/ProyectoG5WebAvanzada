using API.Models;
using API.Repository.AutenticacionRepository;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacionController : Controller
    {
        private readonly IUtilitarios _utilitarios;
        private readonly IAutenticacionRepository _autenticacionRepository;

        public AutenticacionController(IUtilitarios utilitarios, IAutenticacionRepository autenticacionRepository)
        {
            _utilitarios = utilitarios;
            _autenticacionRepository = autenticacionRepository;
        }

        //Register
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Registro(Autenticacion autenticacion)
        {
            //Encriptar la contraseña antes de guardarla (Se hace en el front pero se hace temporalmente
            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna);

            int resultado = _autenticacionRepository.Register(autenticacion);
            if (resultado > 0)
                return Ok(_utilitarios.RespuestaCorrecta(autenticacion));
            else
                return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue registrada correctamente"));
        }

        //Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(Autenticacion autenticacion)
        {
            //Encriptar la contraseña antes de guardarla (Se hace en el front pero se hace temporalmente
            autenticacion.Contrasenna = _utilitarios.Encrypt(autenticacion.Contrasenna);
            var resultado = _autenticacionRepository.Login(autenticacion);
            if (resultado != null)
            {
                resultado.Token = _utilitarios.GenerarToken(resultado.IdUsuario);
                return Ok(_utilitarios.RespuestaCorrecta(resultado));
            }
            return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada correctamente"));
        }

        [HttpPost]
        [Route("RecoverAccess")]
        [AllowAnonymous]
        //Recuperacion de contraseña
        public IActionResult RecoverAccess(Autenticacion autenticacion)
        {
            var resultado = _autenticacionRepository.RecoverAcces(autenticacion);
            if (resultado != null)
            {
                var ContrasennaNotificar = _utilitarios.GenerarContrasena();
                var Contrasenna = _utilitarios.Encrypt(ContrasennaNotificar);

                var resultadoActualizado = _autenticacionRepository.UpdatePasswordLost(resultado, Contrasenna);

                if (resultadoActualizado > 0)
                {
                    _utilitarios.SMTPCorreo(resultado, ContrasennaNotificar);
                    return Ok(_utilitarios.RespuestaCorrecta(resultado));
                }
            }
            return BadRequest(_utilitarios.RespuestaIncorrecta("Su información no fue validada correctamente"));
        }
    }

        //Cambio de contraseña en mi Perfil

        //Actualizacion de datos en mi Perfil
}

