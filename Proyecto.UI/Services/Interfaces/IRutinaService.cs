using Proyecto.UI.Models;

namespace Proyecto.UI.Services.Interfaces
{
    public interface IRutinaService
    {
        Task<List<Rutina>> ObtenerRutinasAsync();
        Task<Rutina> ObtenerRutinaPorIdAsync(int id);
        Task<bool> InsertarRutinaAsync(Rutina rutina);
        Task<bool> ActualizarRutinaAsync(Rutina rutina);
        Task<bool> EliminarRutinaAsync(int id);
    }
}