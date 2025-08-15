using API.Models;

namespace API.Repository.ProductoRepository
{
    public interface IProductoRepository
    {
        //Funcion para ambas vistas
        List<Producto> ObtenerTodos();

        //Funcion para vista Usuario

        List<Producto> ObtenerTodosFiltradoXCategoria(int categoriaId);

        //Funciones de la vista Administrador
        int ActualizarProducto(Producto data);

        Producto CrearProducto(Producto data);

        bool EliminarProducto(int idProducto);

    }
}
