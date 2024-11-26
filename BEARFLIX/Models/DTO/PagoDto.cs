namespace BEARFLIX.Models.DTO
{
    public class PagoDto
    {
        public int IdUsuario { get; set; }
        public int IdPelicula { get; set; }
        public decimal Monto { get; set; }
        public int IdTipo { get; set; } 
    }

}
