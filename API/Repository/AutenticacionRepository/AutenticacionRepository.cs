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

        public bool ExisteCedula(string cedula)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
            connection.Open();
            var query = "SELECT COUNT(*) FROM Usuario WHERE Cedula = @Cedula";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Cedula", cedula);
            var count = (int)command.ExecuteScalar();
            return count > 0;
        }

        public bool ExisteCorreo(string correo)
        {
            using var connection = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
            connection.Open();
            var query = "SELECT COUNT(*) FROM Usuario WHERE CorreoElectronico = @Correo";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Correo", correo);
            var count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }
}
