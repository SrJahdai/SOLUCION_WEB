using BEARFLIX.Models.BD;
using BEARFLIX.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEARFLIX.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly BearflixContext _context;

        public AdministradorController(BearflixContext context)
        {
            _context = context;
        }

        public IActionResult Puntaje()
        {
          
            ViewData["Layout"] = "~/Views/Shared/AdminLayout.cshtml";
            return View();
        }

        // Acción para mostrar la lista de proveedores
        public IActionResult Proveedores()
        {
            var proveedores = _context.Proveedor.ToList();
            ViewData["Layout"] = "~/Views/Shared/AdminLayout.cshtml";
            return View(proveedores);
        }

        // Acción para mostrar la lista de géneros
        public IActionResult Generos()
        {
            var generos = _context.Genero.ToList();
            ViewData["Layout"] = "~/Views/Shared/AdminLayout.cshtml";
            return View(generos);
        }

        // Acción para crear un nuevo proveedor
        [HttpPost]
        public IActionResult CrearProveedor(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Proveedor.Add(proveedor);
                _context.SaveChanges();
                return RedirectToAction("Proveedores");
            }

            return View("Proveedores", _context.Proveedor.ToList());
        }

        // Acción para crear un nuevo género
        [HttpPost]
        public IActionResult CrearGenero(Genero genero)
        {
            if (ModelState.IsValid)
            {
                _context.Genero.Add(genero);
                _context.SaveChanges();
                return RedirectToAction("Generos");
            }

            return View("Generos", _context.Genero.ToList());
        }

        // Acción para eliminar un proveedor
        public IActionResult EliminarProveedor(int id)
        {
            var proveedor = _context.Proveedor.Find(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
                _context.SaveChanges();
            }
            return RedirectToAction("Proveedores");
        }

        // Acción para eliminar un género
        public IActionResult EliminarGenero(int id)
        {
            var genero = _context.Genero.Find(id);
            if (genero != null)
            {
                _context.Genero.Remove(genero);
                _context.SaveChanges();
            }
            return RedirectToAction("Generos");
        }


        // Acción para mostrar todas las películas
        public IActionResult Index()
        {
            ViewData["Layout"] = "~/Views/Shared/AdminLayout.cshtml";
            var peliculas = _context.Pelicula.ToList();
            return View(peliculas);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var pelicula = _context.Pelicula
                .Include(p => p.IdGenero)
                .FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            var peliculaDto = new PeliculaEdicionDto
            {
                Id = pelicula.Id,
                Titulo = pelicula.Titulo,
                Descripcion = pelicula.Descripcion,
                Duracion = pelicula.Duracion,
                PrecioCompra = pelicula.PrecioCompra,
                PrecioRenta = pelicula.PrecioRenta,
                IdProveedor = pelicula.IdProveedor,
                IdGeneros = pelicula.IdGenero.Select(g => g.Id).ToList()
            };

            ViewBag.Proveedores = _context.Proveedor.ToList();
            ViewBag.Generos = _context.Genero.ToList();
            ViewData["Layout"] = "~/Views/Shared/AdminLayout.cshtml";

            return View(peliculaDto);
        }

        [HttpPost]
        public IActionResult Editar(int id, [FromBody] PeliculaEdicionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos no válidos");
            }

            var pelicula = _context.Pelicula
                .Include(p => p.IdGenero)
                .FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            pelicula.Titulo = dto.Titulo;
            pelicula.Descripcion = dto.Descripcion;
            pelicula.Duracion = dto.Duracion;
            pelicula.PrecioCompra = dto.PrecioCompra;
            pelicula.PrecioRenta = dto.PrecioRenta;
            pelicula.IdProveedor = dto.IdProveedor;

            // Limpiar los géneros actuales y agregar los nuevos
            pelicula.IdGenero.Clear();
            foreach (var generoId in dto.IdGeneros)
            {
                var genero = _context.Genero.Find(generoId);
                if (genero != null)
                {
                    pelicula.IdGenero.Add(genero);
                }
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(new { message = "Película actualizada correctamente" });
        }

        // Acción para eliminar una película
        public IActionResult Eliminar(int id)
        {
            var pelicula = _context.Pelicula
                .Include(p => p.IdGenero)
                .FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Pelicula.Remove(pelicula);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
