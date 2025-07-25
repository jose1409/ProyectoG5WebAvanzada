using System.Data;
using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repository.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IConfiguration _configuration;

        public ProductoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Producto> ObtenerTodos()
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Query<Producto>("ObtenerProductos", commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }
        }

        public List<Producto> ObtenerTodosFiltradoXCategoria(int categoriaId)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Query<Producto>(
                    "ObtenerProductosFiltradoXCategoria",
                    new { IdCategoria = categoriaId },
                    commandType: CommandType.StoredProcedure
                ).ToList();
                return resultado;
            }
        }
    }
}
