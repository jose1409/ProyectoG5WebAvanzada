using API.Models;

namespace API.Repository.CategoriaRepository
{
    public interface ICategoriaRepository
    {
        List<Categoria> ObtenerTodos();

        Categoria ObtenerPorId(int idCategoria);

        Categoria Crear(Categoria categoria);

        Categoria Actualizar(Categoria categoria);

        bool Eliminar(int idCategoria);

    }
}
