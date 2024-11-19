namespace BEARFLIX.Models.DTO
{
    public class PeliculaDetallesDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string Portada { get; set; }
        public string Fondo { get; set; }
        public string Estreno { get; set; }
        public List<string> Generos { get; set; }
        public string Video { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioRenta { get; set; }
        public double PuntajePromedio { get; set; }
        public int TotalPuntajes { get; set; }
    }

}