using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using API.Models;

namespace API.Repository.CarritoRepository
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly IDbConnection _db;
        public CarritoRepository(IDbConnection db) => _db = db;

        public async Task<CarritoDto> ObtenerAsync(int idUsuario)
        {
            var items = (await _db.QueryAsync<CarritoItemDto>(
                "dbo.spCarrito_Obtener",
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure)).ToList();

            return new CarritoDto { Items = items };
        }

        public Task<int> AgregarAsync(int idUsuario, int idProducto, int cantidad) =>
            _db.ExecuteScalarAsync<int>("dbo.spCarrito_AgregarItem",
                new { IdUsuario = idUsuario, IdProducto = idProducto, Cantidad = cantidad },
                commandType: CommandType.StoredProcedure);

        public Task<int> ActualizarCantidadAsync(int idDetalle, int cantidad) =>
            _db.ExecuteScalarAsync<int>("dbo.spCarrito_ActualizarCantidad",
                new { IdDetalle = idDetalle, Cantidad = cantidad },
                commandType: CommandType.StoredProcedure);

        public Task<int> EliminarItemAsync(int idDetalle) =>
            _db.ExecuteScalarAsync<int>("dbo.spCarrito_EliminarItem",
                new { IdDetalle = idDetalle },
                commandType: CommandType.StoredProcedure);

        public async Task<int> VaciarAsync(int idUsuario)
        {
            var ok = await _db.ExecuteScalarAsync<int?>(
                "dbo.spCarrito_Vaciar",
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);

            return ok ?? 0;
        }

        public Task<int> CheckoutAsync(int idUsuario) =>
            _db.ExecuteScalarAsync<int>("dbo.spPedidos_CrearDesdeCarrito",
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure);
    }
}
