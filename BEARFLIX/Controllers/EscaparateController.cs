using Microsoft.AspNetCore.Mvc;
using BEARFLIX.Models.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BEARFLIX.Models.DTO;
using System.Security.Claims;

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

        [HttpGet("Detalles/{id}")]
        public async Task<IActionResult> GetPeliculaDetalles(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado." });
            }

            int idUsuario = int.Parse(userId);

            var pelicula = await _context.Pelicula
                .Include(p => p.IdGenero.Where(g => g != null))
                .Include(p => p.Puntaje)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound(new { mensaje = "Película no encontrada." });
            }

            // Verificar si el usuario ya tiene una compra o renta activa
            var ventaExistente = await _context.Venta
                .Where(v => v.IdUsuario == idUsuario && v.IdPelicula == id)
                .OrderByDescending(v => v.FechaVenta)
                .FirstOrDefaultAsync();

            bool mostrarVideo = ventaExistente != null &&
                                (ventaExistente.IdTipo == 1 ||
                                 (ventaExistente.IdTipo == 2 && ventaExistente.Expiracion >= DateTime.Now));

            var puntajePromedio = pelicula.Puntaje.Any()
                ? pelicula.Puntaje.Average(p => p.Puntaje1)
                : 0;

            var respuesta = new
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Descripcion = pelicula.Descripcion,
                Duracion = pelicula.Duracion,
                Portada = pelicula.Portada,
                Fondo = pelicula.Fondo,
                Estreno = pelicula.Estreno.ToString("yyyy-MM-dd"),
                Generos = pelicula.IdGenero.Select(g => g.Descripcion).ToList(),
                Video = pelicula.Video, // URL del video
                PrecioCompra = pelicula.PrecioCompra,
                PrecioRenta = pelicula.PrecioRenta,
                PuntajePromedio = Math.Round(puntajePromedio, 2),
                TotalPuntajes = pelicula.Puntaje.Count,
                MostrarBotones = !mostrarVideo, // Mostrar botones si no tiene compra/renta activa
                MostrarVideo = mostrarVideo    // Mostrar video si ya lo compró/rentó
            };

            return Ok(respuesta);
        }


        [HttpPost("RealizarPago")]
        public async Task<IActionResult> RealizarPago([FromBody] PagoDto pagoDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado." });
            }

            int idUsuario = int.Parse(userId);
            int idPelicula = pagoDto.IdPelicula;
            decimal monto = pagoDto.Monto;
            int idTipo = pagoDto.IdTipo; // 1: compra, 2: renta

            try
            {
                var venta = new Venta
                {
                    IdUsuario = idUsuario,
                    IdPelicula = idPelicula,
                    FechaVenta = DateTime.Now,
                    Monto = monto,
                    IdTipo = idTipo,
                    Expiracion = idTipo == 2 ? DateTime.Now.AddDays(7) : null // Expiración para renta (7 días)
                };

                _context.Venta.Add(venta);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Compra o renta realizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }


        [HttpPost("GuardarPuntaje")]
        public async Task<IActionResult> GuardarPuntaje([FromBody] PuntajeDto puntajeDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado." });
            }

            int idUsuario = int.Parse(userId);
            int idPelicula = puntajeDto.IdPelicula;
            int puntaje = puntajeDto.Puntaje;

            if (puntaje < 1 || puntaje > 5)
            {
                return BadRequest(new { mensaje = "El puntaje debe estar entre 1 y 5." });
            }

            // Verificar que la película existe
            var peliculaExistente = await _context.Pelicula.FindAsync(idPelicula);
            if (peliculaExistente == null)
            {
                return BadRequest(new { mensaje = "La película no existe." });
            }

            try
            {
                var puntajeExistente = await _context.Puntaje
                    .FirstOrDefaultAsync(p => p.IdUsuario == idUsuario && p.IdPelicula == idPelicula);

                if (puntajeExistente != null)
                {
                    puntajeExistente.Puntaje1 = (byte)puntaje;
                    _context.Update(puntajeExistente);
                }
                else
                {
                    var nuevoPuntaje = new Puntaje
                    {
                        IdUsuario = idUsuario,
                        IdPelicula = idPelicula,
                        Puntaje1 = (byte)puntaje
                    };
                    _context.Puntaje.Add(nuevoPuntaje);
                }

                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Puntaje guardado correctamente." });
            }
            catch (Exception ex)
            {
                // Captura de excepciones específicas si es necesario
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
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



    }
}