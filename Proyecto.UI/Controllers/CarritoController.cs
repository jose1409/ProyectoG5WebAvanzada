using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class CarritoController : Controller
{
    
    [HttpPost]
    public IActionResult Agregar(int idProducto)
    {
        
        TempData["Mensaje"] = $"Producto {idProducto} agregado al carrito";

        
        return RedirectToAction("Catalogo", "Productos");
    }
}

