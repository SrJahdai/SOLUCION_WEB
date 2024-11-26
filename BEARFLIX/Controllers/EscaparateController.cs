using Microsoft.AspNetCore.Mvc;
using BEARFLIX.Models.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BEARFLIX.Models.DTO;

namespace BEARFLIX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "USUARIO, TESTER")]
    public class EscaparateController : ControllerBase
    {
        private readonly BearflixContext _context;

        public EscaparateController(BearflixContext context)
        {
            _context = context;
        }

        [HttpGet("escaparateindex")]
        public async Task<IActionResult> GetPeliculasEscaparate()
        {
            var estrenos = await _context.Pelicula
                .Include(p => p.IdGenero.Where(g => g != null)) // Evitar géneros nulos
                .Where(p => p.Estreno > DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)))
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Estreno = p.Estreno,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Where(g => g != null).Select(g => g.Descripcion).ToList()
                })
                .Take(15)
                .ToListAsync();

            var recientes = await _context.Pelicula
                .Include(p => p.IdGenero.Where(g => g != null))
                .OrderByDescending(p => p.Estreno)
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Estreno = p.Estreno,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Where(g => g != null).Select(g => g.Descripcion).ToList()
                })
                .Take(15)
                .ToListAsync();

            var peliculasPorGenero = await _context.Genero
                .Where(g => g.IdPelicula.Any()) // Ignorar géneros sin películas
                .Select(g => new
                {
                    Genero = g.Descripcion,
                    Peliculas = g.IdPelicula
                        .Where(p => p.IdGenero.Any(gen => gen != null))
                        .Select(p => new PeliculaDto
                        {
                            Id = p.Id,
                            Titulo = p.Titulo,
                            Descripcion = p.Descripcion,
                            Estreno = p.Estreno,
                            Portada = p.Portada,
                            Generos = p.IdGenero.Where(gen => gen != null).Select(gen => gen.Descripcion).ToList()
                        })
                        .Take(15)
                        .ToList()
                })
                .ToListAsync();

            return Ok(new
            {
                estrenos,
                recientes,
                peliculasPorGenero
            });
        }

        [HttpGet("todas")]
        public async Task<IActionResult> GetTodasPeliculas()
        {
            var peliculas = await _context.Pelicula
                .Include(p => p.IdGenero.Where(g => g != null))
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Estreno = p.Estreno,
                    Descripcion = p.Descripcion,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Where(g => g != null).Select(g => g.Descripcion).ToList()
                })
                .ToListAsync();

            return Ok(peliculas);
        }

        [HttpGet("estrenos")]
        public async Task<IActionResult> GetEstrenos()
        {
            var peliculas = await ObtenerPeliculasPorFecha("estreno", 5);
            return Ok(peliculas);
        }

        [HttpGet("generos")]
        public async Task<IActionResult> GetGeneros()
        {
            var generos = await _context.Genero
                .Where(g => g.IdPelicula.Any()) // Ignorar géneros sin películas
                .Select(g => new GeneroDto
                {
                    Id = g.Id,
                    Descripcion = g.Descripcion
                })
                .ToListAsync();

            return Ok(generos);
        }

        private async Task<List<PeliculaDto>> ObtenerPeliculasPorFecha(string fechaPropiedad, int limite)
        {
            return await _context.Pelicula
                .OrderByDescending(p => EF.Property<DateTime>(p, fechaPropiedad))
                .Select(p => new PeliculaDto
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Estreno = p.Estreno,
                    Descripcion = p.Descripcion,
                    Portada = p.Portada,
                    Generos = p.IdGenero.Where(g => g != null).Select(g => g.Descripcion).ToList()
                })
                .Take(limite)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeliculaDetalles(int id)
        {
            var pelicula = await _context.Pelicula
                .Include(p => p.IdGenero.Where(g => g != null))
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
