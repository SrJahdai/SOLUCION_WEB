using System.ComponentModel.DataAnnotations;

namespace BEARFLIX.Models.DTO
{
    
    public class LoginDto
    {
        public required string Correo { get; set; }
        public required string Contrasena { get; set; }
        public required bool Recuerdame { get; set; }


    }

}