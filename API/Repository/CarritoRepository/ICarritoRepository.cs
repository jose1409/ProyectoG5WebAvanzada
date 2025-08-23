using System.Threading.Tasks;
using API.Models;

namespace API.Repository.CarritoRepository
{
    public interface ICarritoRepository
    {
        Task<CarritoDto> ObtenerAsync(int idUsuario);
        Task<int> AgregarAsync(int idUsuario, int idProducto, int cantidad);
        Task<int> ActualizarCantidadAsync(int idDetalle, int cantidad);
        Task<int> EliminarItemAsync(int idDetalle);
        Task<int> VaciarAsync(int idUsuario);
        Task<int> CheckoutAsync(int idUsuario);
    }
}

