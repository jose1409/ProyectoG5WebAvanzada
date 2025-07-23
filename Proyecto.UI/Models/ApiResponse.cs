namespace Proyecto.UI.Models
{
    public class ApiResponse
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
        public object? Contenido { get; set; }
    }

    public class ApiResponse<T>
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; }
        public T? Contenido { get; set; }
    }
}
