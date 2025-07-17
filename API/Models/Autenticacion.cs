namespace API.Models
{
    public class Autenticacion
    {
        public int IdUsuario { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasenna { get; set; }
        public string? Telefono { get; set; }
        public byte[]? Fotografia { get; set; }
        public bool? Activo { get; set; }
        public string? Token { get; set; }
    }
}
