using Microsoft.AspNetCore.Mvc;
using BEARFLIX.Models.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BEARFLIX.Models.DTO;

namespace BEARFLIX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("USUARIO, TESTER")]
    public class EscaparateController : ControllerBase
    {
        private readonly BearflixContext _context;

        public EscaparateController(BearflixContext context)
        {
            _context = context;
        }

        // Obtener todas las películas
        [HttpGet("todas")]
        public async Task<IActionResult> GetTodasPeliculas()
        {
            var peliculas = await _context.Pelicula
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Estreno = p.Estreno,
                    Descripcion = p.Descripcion,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Select(g => g.Descripcion).ToList()
                })
                .ToListAsync();

            return Ok(peliculas);
        }

        // Obtener las 5 películas más recientes por fecha de estreno
        [HttpGet("estrenos")]
        public async Task<IActionResult> GetEstrenos()
        {
            var peliculas = await _context.Pelicula
                .OrderByDescending(p => p.Estreno)
                .Take(5)
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Estreno = p.Estreno,
                    Descripcion = p.Descripcion,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Select(g => g.Descripcion).ToList()
                })
                .ToListAsync();

            return Ok(peliculas);
        }

        // Obtener todos los géneros
        [HttpGet("generos")]
        public async Task<IActionResult> GetGeneros()
        {
            var generos = await _context.Genero
                .Select(g => new GeneroDto
                {
                    Id = g.Id,
                    Descripcion = g.Descripcion
                })
                .ToListAsync();

            return Ok(generos);
        }

        // Obtener 15 películas según los géneros
        [HttpGet("peliculas-por-genero")]
        public async Task<IActionResult> GetPeliculasPorGenero()
        {
            var peliculasPorGenero = await _context.Pelicula
                .Include(p => p.IdGenero)
                .SelectMany(p => p.IdGenero, (pelicula, genero) => new { pelicula, genero })
                .GroupBy(pg => pg.genero.Id)
                .SelectMany(g => g.Take(3)) // Tomar hasta 3 películas por género
                .Take(15) // Limitar a 15 películas
                .Select(pg => new PeliculaDto
                {
                    Id = pg.pelicula.Id,
                    Titulo = pg.pelicula.Titulo,
                    Estreno = pg.pelicula.Estreno,
                    Descripcion = pg.pelicula.Descripcion,
                    Portada = pg.pelicula.Portada,
                    Generos = pg.pelicula.IdGenero.Select(g => g.Descripcion).ToList()
                })
                .ToListAsync();

            return Ok(peliculasPorGenero);
        }

        // Obtener las 15 películas más recientes por fecha de registro
        [HttpGet("recientes")]
        public async Task<IActionResult> GetPeliculasMasRecientes()
        {
            var peliculas = await _context.Pelicula
                .OrderByDescending(p => EF.Property<DateTime>(p, "FechaRegistro")) // Cambia "FechaRegistro" por el nombre correcto del campo
                .Take(15)
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Estreno = p.Estreno,
                    Descripcion = p.Descripcion,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Select(g => g.Descripcion).ToList()
                })
                .ToListAsync();

            return Ok(peliculas);
        }

        [HttpGet("{genero}/peliculas")]
        public async Task<IActionResult> GetPeliculasPorGenero(string genero)
        {
            var peliculas = await _context.Pelicula
                .Include(p => p.IdGenero)
                .Where(p => p.IdGenero.Any(g => g.Descripcion.ToLower() == genero.ToLower()))
                .Select(p => new
                {
                    p.Id,
                    p.Titulo,
                    p.Portada,
                    Estreno = p.Estreno.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            if (!peliculas.Any())
            {
                return NotFound(new { mensaje = $"No se encontraron películas para el género '{genero}'." });
            }

            return Ok(peliculas);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeliculaDetalles(int id)
        {
            var pelicula = await _context.Pelicula
                .Include(p => p.IdGenero)
                .Include(p => p.Puntaje)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound(new { mensaje = "Película no encontrada." });
            }

            var puntajePromedio = pelicula.Puntaje.Any()
                ? pelicula.Puntaje.Average(p => p.Puntaje1)
                : 0;

            var respuesta = new PeliculaDetallesDto
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Descripcion = pelicula.Descripcion,
                Duracion = pelicula.Duracion,
                Portada = pelicula.Portada,
                Fondo = pelicula.Fondo,
                Estreno = pelicula.Estreno.ToString("yyyy-MM-dd"),
                Generos = pelicula.IdGenero.Select(g => g.Descripcion).ToList(),
                Video = pelicula.Video,
                PrecioCompra = pelicula.PrecioCompra,
                PrecioRenta = pelicula.PrecioRenta,
                PuntajePromedio = Math.Round(puntajePromedio, 2),
                TotalPuntajes = pelicula.Puntaje.Count
            };

            return Ok(respuesta);
        }

    }
}
