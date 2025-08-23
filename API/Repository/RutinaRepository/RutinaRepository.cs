using API.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace API.Repository.RutinaRepository
{
    public class RutinaRepository : IRutinaRepository
    {
        private readonly IConfiguration _configuration;

        public RutinaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Rutina> ObtenerRutinas()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var rutinas = connection.Query<Rutina>("ObtenerRutinas", commandType: CommandType.StoredProcedure).ToList();
            return rutinas;
        }

        public async Task<RutinaConProductos?> ObtenerRutinaPorId(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var diccionario = new Dictionary<int, RutinaConProductos>();

            var resultado = await connection.QueryAsync<RutinaConProductos, ProductoRutina, RutinaConProductos>(
                "ObtenerRutinaPorId",
                (rutina, producto) =>
                {
                    if (!diccionario.TryGetValue(rutina.IdRutina, out var rutinaExistente))
                    {
                        rutinaExistente = rutina;
                        rutinaExistente.Productos = new List<ProductoRutina>();

                        // Convertir imagen binaria a base64
                        if (rutina.Imagen is byte[] binario)
                        {
                            rutinaExistente.RutaImagen = $"data:image/png;base64,{Convert.ToBase64String(binario)}";
                        }
                        else
                        {
                            rutinaExistente.RutaImagen = "";
                        }

                        diccionario.Add(rutina.IdRutina, rutinaExistente);
                    }

                    if (producto != null && producto.IdProducto != 0)
                    {
                        if (producto.ImagenBinaria is byte[] bin)
                        {
                            producto.RutaImagen = $"data:image/png;base64,{Convert.ToBase64String(bin)}";
                        }
                        else
                        {
                            producto.RutaImagen = "";
                        }

                        rutinaExistente.Productos.Add(producto);
                    }

                    return rutinaExistente;
                },
                new { IdRutina = id },
                splitOn: "IdProducto",
                commandType: CommandType.StoredProcedure
            );

            return resultado.FirstOrDefault();
        }

        public int InsertarRutina(Rutina rutina)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("Nombre", rutina.Nombre);
            parameters.Add("Descripcion", rutina.Descripcion);
            parameters.Add("Imagen", rutina.Imagen);
            parameters.Add("IdGenerado", dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("InsertarRutina", parameters, commandType: CommandType.StoredProcedure);
            var idGenerado = parameters.Get<int>("IdGenerado");

            if (rutina.IdsProductos != null)
            {
                foreach (var idProducto in rutina.IdsProductos)
                {
                    connection.Execute("InsertarProductoEnRutina", new
                    {
                        IdRutina = idGenerado,
                        id_producto = idProducto
                    }, commandType: CommandType.StoredProcedure);
                }
            }

            return idGenerado;
        }

        public int ActualizarRutina(Rutina rutina)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var parameters = new
            {
                rutina.IdRutina,
                rutina.Nombre,
                rutina.Descripcion,
                rutina.Imagen
            };

            var result = connection.Execute("ActualizarRutina", parameters, commandType: CommandType.StoredProcedure);

            // Eliminar productos actuales
            connection.Execute("EliminarProductosDeRutina", new { rutina.IdRutina }, commandType: CommandType.StoredProcedure);

            // Insertar nuevos productos
            if (rutina.IdsProductos != null)
            {
                foreach (var idProducto in rutina.IdsProductos)
                {
                    connection.Execute("InsertarProductoEnRutina", new
                    {
                        IdRutina = rutina.IdRutina,
                        id_producto = idProducto
                    }, commandType: CommandType.StoredProcedure);
                }
            }

            return result;
        }

        public int EliminarRutina(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection.Execute("EliminarRutina", new { IdRutina = id }, commandType: CommandType.StoredProcedure);
        }

    }
}