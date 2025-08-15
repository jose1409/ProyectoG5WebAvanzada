using Proyecto.UI.Models;

namespace Proyecto.UI.Repository.ProductoRepository
{
    public interface IProductoRepository
    {
        //Funciones de la vista Administrador
        int ActualizarProducto(Producto data);

        Producto CrearProducto(Producto data);

        bool EliminarProducto(int idProducto);

        List<Producto> ObtenerTodos();
    }
}
