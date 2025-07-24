using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;
namespace API.Repository.AutenticacionRepository
{
    public class AutenticacionRepository : IAutenticacionRepository
    {
        private readonly IConfiguration _configuration;

        public AutenticacionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Autenticacion Login(Autenticacion autenticacion)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.QueryFirstOrDefault<Autenticacion>("Login", new
                {
                    autenticacion.CorreoElectronico,
                    autenticacion.Contrasenna
                });
                return resultado;
            }
        }

        public Autenticacion RecoverAcces(Autenticacion autenticacion)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.QueryFirstOrDefault<Autenticacion>("ValidarCorreo", new
                {
                    autenticacion.CorreoElectronico
                });
                return resultado;
            }
        }

        public int Register(Autenticacion autenticacion)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Execute("RegistrarUsuario", new
                {
                    autenticacion.CorreoElectronico,
                    autenticacion.Contrasenna,
                    autenticacion.Cedula,
                    autenticacion.Nombre,
                    autenticacion.Apellidos,
                });
                return resultado;
            }
        }

        public int UpdatePasswordLost(Autenticacion autenticacion, String Contrasenna)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultadoActuallizacion = contexto.Execute("ActualizarContrasenna", new
                {
                    autenticacion.IdUsuario,
                    Contrasenna
                });
                return resultadoActuallizacion;
            }
        }
    }
}
