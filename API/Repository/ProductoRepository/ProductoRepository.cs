using System.Data;
using System.Linq;
using System.Collections.Generic;
using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Repository.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IConfiguration _configuration;

        public ProductoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int ActualizarProducto(Producto data)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Execute("EditarProducto", new
                {
                    data.IdProducto,
                    data.IdCategoria,
                    data.Descripcion,
                    data.Detalle,
                    data.RutaImagen,
                    data.Precio,
                    data.Activo
                });
                return resultado;
            }
        }

        public Producto CrearProducto(Producto data)
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.QueryFirstOrDefault<Producto>(
                    "CrearProducto",
                    new
                    {
                        data.Descripcion,
                        data.Detalle,
                        data.Precio,
                        data.IdCategoria,
                        data.RutaImagen,
                        data.Activo
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado!;
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            using (var conexion = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = conexion.Execute(
                    "EliminarProducto",
                    new { idProducto },
                    commandType: CommandType.StoredProcedure
                );
                return resultado > 0;
            }
        }

        public List<Producto> ObtenerTodos()
        {
            using (var contexto = new SqlConnection(_configuration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var resultado = contexto.Query<Producto>(
                    "ObtenerProductos",
                    commandType: CommandType.StoredProcedure
                ).ToList();
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
