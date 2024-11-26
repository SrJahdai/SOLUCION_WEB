using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BEARFLIX.Models.DTO
{
    public class PeliculaRegistroDto
    {
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required int Duracion { get; set; }
        public required IFormFile ImagenPrincipal { get; set; }
        public required IFormFile ImagenFondo { get; set; }
        public required IFormFile ImagenTitulo { get; set; }
        public required IFormFile VideoArchivo { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioRenta { get; set; }
        public int IdProveedor { get; set; } // Usar el ID del proveedor
        public List<int> IdGeneros { get; set; } // Usar lista de IDs de géneros
        public DateOnly Estreno { get; set; } // Ahora es una fecha
    }
}
