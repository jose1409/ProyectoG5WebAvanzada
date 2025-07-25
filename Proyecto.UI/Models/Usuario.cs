namespace Proyecto.UI.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string CorreoElectronico { get; set; } = string.Empty;
        public string? Contrasenna { get; set; }
        public string? Telefono { get; set; }
        public byte[]? Fotografia { get; set; }
        public bool? Activo { get; set; }
    }
}
