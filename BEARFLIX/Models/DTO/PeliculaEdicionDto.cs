namespace BEARFLIX.Models.DTO
{
    public class PeliculaEdicionDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioRenta { get; set; }
        public int IdProveedor { get; set; }
        public List<int> IdGeneros { get; set; } = new List<int>();
    }
}
