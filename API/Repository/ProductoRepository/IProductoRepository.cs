using System.Collections.Generic;
using API.Models;

namespace API.Repository.ProductoRepository
{
    public interface IProductoRepository
    {
        // Funciones comunes
        List<Producto> ObtenerTodos();
        List<Producto> ObtenerTodosFiltradoXCategoria(int categoriaId);

        // Funciones de la vista Administrador
        int ActualizarProducto(Producto data);
        Producto CrearProducto(Producto data);
        bool EliminarProducto(int idProducto);
        List<Producto> ObtenerPorCategoria(int idCategoria);
    }
}

