using System.Data;
using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repository.CategoriaRepository
{
    public class CategoriaRepository: ICategoriaRepository
    {
        private readonly IConfiguration _configuration;

        public CategoriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Categoria Actualizar(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Categoria Crear(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int idCategoria)
        {
            throw new NotImplementedException();
        }

        public Categoria ObtenerPorId(int idCategoria)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> ObtenerTodos()
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Query<Categoria>("ObtenerCategorias", commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }
    }
}
