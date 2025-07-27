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

        public int Actualizar(Categoria categoria)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Execute("EditarCategoria", new
                {
                    categoria.IdCategoria,
                    categoria.Descripcion,
                    categoria.Imagen,
                    categoria.Activo
                });
                return resultado;
            }
        }

        public Categoria Crear(Categoria categoria)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.QueryFirstOrDefault<Categoria>(
                    "CrearCategoria",
                    new
                    {
                        descripcion = categoria.Descripcion,
                        ruta_imagen = categoria.Imagen,
                        activo = categoria.Activo
                    },
                    commandType: CommandType.StoredProcedure
                );

                return resultado!;
            }
        }

        public bool Eliminar(int idCategoria)
        {
            using (var conexion = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = conexion.Execute(
                    "EliminarCategoria",
                    new { id_categoria = idCategoria },
                    commandType: CommandType.StoredProcedure
                );
                return resultado > 0;
            }
        }

        public Categoria ObtenerPorId(int idCategoria)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> ObtenerPorNombre(string nombreCategoria)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Query<Categoria>(
                    "ObtenerPorNombreCategoria",
                    new { descripcion = nombreCategoria },
                    commandType: CommandType.StoredProcedure
                ).ToList();
                return resultado;
            }
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
