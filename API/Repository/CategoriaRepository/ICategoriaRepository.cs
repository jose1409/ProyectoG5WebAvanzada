using API.Models;

namespace API.Repository.CategoriaRepository
{
    public interface ICategoriaRepository
    {
        List<Categoria> ObtenerTodos();

        Categoria ObtenerPorId(int idCategoria);

        Categoria Crear(Categoria categoria);

        int Actualizar(Categoria categoria);

        bool Eliminar(int idCategoria);

        List<Categoria> ObtenerPorNombre(string nombreCategoria);
    }
}
