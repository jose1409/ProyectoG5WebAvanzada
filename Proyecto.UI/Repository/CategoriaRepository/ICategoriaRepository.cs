using Proyecto.UI.Models;

namespace Proyecto.UI.Repository.CategoriaRepository
{
    public interface ICategoriaRepository
    {
        int ActualizarCategoria(Categoria categoria);

        Categoria CrearCategoria(Categoria categoria);

        bool EliminarCategoria(int idCategoria);

        List<Categoria> ObtenerTodasCategorias();

        Task<List<Categoria>> ObtenerCategoriaPoNombred(string descripcion);

        Task<Categoria> ObtenerPorId(int idCategoria);
    }
}
