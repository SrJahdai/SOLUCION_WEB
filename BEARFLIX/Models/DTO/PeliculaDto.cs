namespace BEARFLIX.Models.DTO
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateOnly Estreno { get; set; }
        public string Portada { get; set; }
        public List<string> Generos { get; set; }
        public byte? PuntajeUsuario { get; set; }
    }

}
