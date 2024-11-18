using Microsoft.AspNetCore.Mvc;
using BEARFLIX.Filters;
using BEARFLIX.Models.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace BEARFLIX.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize("USUARIO, TESTER")]
    public class PeliculaController : ControllerBase
    {
        private readonly BearflixContext _context;

        public PeliculaController(BearflixContext context)
        {
            _context = context;
        }

        [HttpGet("todas")]
        public async Task<IActionResult> GetTodasPeliculas()
        {
            var peliculas = await _context.Pelicula.ToListAsync();
            return Ok(peliculas);
        }

        [HttpGet("estrenos")]
        public async Task<IActionResult> GetEstrenos()
        {
            var peliculas = await _context.Pelicula
                .OrderByDescending(p => p.Estreno)
                .Take(5)
                .ToListAsync();
            return Ok(peliculas);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPeliculas([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { mensaje = "Debe proporcionar un término de búsqueda." });
            }

            query = query.ToLower();

            var peliculasPorTitulo = await _context.Pelicula
                .Where(p => EF.Functions.Like(p.Titulo.ToLower(), $"%{query}%"))
                .ToListAsync();

            var peliculasPorGenero = await _context.Pelicula
                .Where(p => p.IdGenero.Any(g => EF.Functions.Like(g.Descripcion.ToLower(), $"%{query}%")))
                .ToListAsync();

            var peliculas = peliculasPorTitulo
                .Union(peliculasPorGenero)
                .Distinct()
                .ToList();

            if (!peliculas.Any())
            {
                return NotFound(new { mensaje = "No se encontraron películas que coincidan con el término de búsqueda." });
            }

            return Ok(peliculas);
        }
    }

}
