namespace API.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public bool Activo { get; set; }
    }
}
