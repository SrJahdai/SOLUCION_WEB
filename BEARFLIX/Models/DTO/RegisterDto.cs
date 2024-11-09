using System.ComponentModel.DataAnnotations;

namespace BEARFLIX.Models.DTO
{
    public class RegisterDto
    {
        [Required]
        [StringLength(150, MinimumLength = 5)]
        public required string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public required string Correo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateOnly FechaNacimiento { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public required string Contrasena { get; set; }

        [Required]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmarContrasena { get; set; }
    }

}
