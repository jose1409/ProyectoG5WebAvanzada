using System.ComponentModel.DataAnnotations;

namespace Proyecto.UI.Models
{
    public class Autenticacion
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        public string? Apellidos { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string? Contrasenna { get; set; }
        public string? Telefono { get; set; }
        public byte[]? Fotografia { get; set; }
        public bool? Activo { get; set; }
        public string? Token { get; set; }

        [Required(ErrorMessage = "Debe aceptar los términos y condiciones")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar los términos y condiciones")]
        [Display(Name = "Aceptar Términos")]
        public bool AceptaTerminos { get; set; }
    }
}
