using API.Models;

namespace API.Repository.RutinaRepository
{
    public interface IRutinaRepository
    {
        List<Rutina> ObtenerRutinas();
        Task<RutinaConProductos?> ObtenerRutinaPorId(int id);
        int InsertarRutina(Rutina rutina);
        int ActualizarRutina(Rutina rutina);
        int EliminarRutina(int id);
    }
}