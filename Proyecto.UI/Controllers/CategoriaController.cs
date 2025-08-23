using Microsoft.AspNetCore.Mvc;
using Proyecto.UI.Models;
using Proyecto.UI.Repository.ProductoRepository;
using Proyecto.UI.Repository.CategoriaRepository;

public class CategoriaController : Controller
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IProductoRepository _productoRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository, IProductoRepository productoRepository)
    {
        _categoriaRepository = categoriaRepository;
        _productoRepository = productoRepository;
    }

    private async Task CargarCategoriasSidebar()
    {
        var categorias = _categoriaRepository.ObtenerTodasCategorias();
        ViewBag.Categorias = categorias;
    }

    public async Task<IActionResult> PorCategoria(int id)
    {
        await CargarCategoriasSidebar();

        var categoria = await _categoriaRepository.ObtenerPorId(id);
        var productos = await _productoRepository.ObtenerPorCategoria(id);

        var viewModel = new CategoriaViewModel
        {
            Descripcion = categoria.Descripcion,
            Imagen = categoria.Imagen,
            Productos = productos
        };

        return View("Categoria", viewModel);
    }
}