using API.Models;

namespace API.Repository.ProductoRepository
{
    public interface IProductoRepository
    {
        List<Producto> ObtenerTodos();

        List<Producto> ObtenerTodosFiltradoXCategoria(int categoriaId);
    }
}
