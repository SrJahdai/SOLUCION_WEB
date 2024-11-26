using BEARFLIX.Models.BD;
using BEARFLIX.Models.DTO;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "ADMINISTRADOR, DUENO")]
public class AdminController : ControllerBase
{
    private readonly Cloudinary _cloudinary;
    private readonly BearflixContext _context;

    public AdminController(Cloudinary cloudinary, BearflixContext context)
    {
        _cloudinary = cloudinary;
        _context = context;
    }

    [HttpGet("generos")]
    public async Task<IActionResult> ObtenerGeneros()
    {
        try
        {
            var generos = await _context.Genero
                .Select(g => new { g.Id, g.Descripcion })
                .ToListAsync();

            return Ok(generos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener los géneros: {ex.Message}");
        }
    }

    [HttpGet("proveedores")]
    public async Task<IActionResult> ObtenerProveedores()
    {
        try
        {
            var proveedores = await _context.Proveedor
                .Select(p => new { p.Id, p.Descripcion })
                .ToListAsync();

            return Ok(proveedores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener los proveedores: {ex.Message}");
        }
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarPelicula([FromForm] PeliculaRegistroDto peliculaDto)
    {
        if (peliculaDto == null)
        {
            return BadRequest("Datos de la película inválidos.");
        }

        var peliculaExistente = await _context.Pelicula
            .FirstOrDefaultAsync(p => p.Titulo.ToLower() == peliculaDto.Titulo.ToLower());

        if (peliculaExistente != null)
        {
            if (peliculaExistente != null)
            {
                return BadRequest(new { success = false, message = "Ya existe una película con el mismo título." });
            }
        }

        // Validar que el proveedor existe usando el ID recibido
        var proveedor = await _context.Proveedor
            .FirstOrDefaultAsync(p => p.Id == peliculaDto.IdProveedor); // Buscar por IdProveedor
        if (proveedor == null)
        {
            return NotFound($"El proveedor con ID {peliculaDto.IdProveedor} no existe.");
        }

        // Subir imágenes y video a Cloudinary
        var portadaUrl = await SubirArchivo(peliculaDto.ImagenPrincipal, "portada");
        var fondoUrl = await SubirArchivo(peliculaDto.ImagenFondo, "fondo");
        var tituloImagenUrl = await SubirArchivo(peliculaDto.ImagenTitulo, "titulo");
        var videoUrl = await SubirArchivo(peliculaDto.VideoArchivo, "video");

        if (portadaUrl == null || fondoUrl == null || tituloImagenUrl == null || videoUrl == null)
        {
            return StatusCode(500, "Error al subir uno o más archivos.");
        }

        // Crear instancia de película
        var nuevaPelicula = new Pelicula
        {
            Titulo = peliculaDto.Titulo,
            Descripcion = peliculaDto.Descripcion,
            Duracion = peliculaDto.Duracion,
            Portada = portadaUrl,
            Fondo = fondoUrl,
            TituloImagen = tituloImagenUrl,
            Estreno = peliculaDto.Estreno,
            Video = videoUrl,
            PrecioCompra = peliculaDto.PrecioCompra,
            PrecioRenta = peliculaDto.PrecioRenta,
            IdProveedor = proveedor.Id // Usar el ID del proveedor validado
        };

        // Agregar géneros relacionados
        if (peliculaDto.IdGeneros != null && peliculaDto.IdGeneros.Any())
        {
            var generosValidos = await _context.Genero
                .Where(g => peliculaDto.IdGeneros.Contains(g.Id)) // Buscar por Id
                .ToListAsync();

            if (generosValidos.Count != peliculaDto.IdGeneros.Count)
            {
                return BadRequest("Algunos de los géneros proporcionados no existen.");
            }

            nuevaPelicula.IdGenero = generosValidos; // Asignar géneros válidos
        }

        try
        {
            _context.Pelicula.Add(nuevaPelicula);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Película registrada correctamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al registrar la película: {ex.Message}");
        }
    }

    private async Task<string> SubirArchivo(IFormFile archivo, string carpeta)
    {
        if (archivo == null || archivo.Length == 0)
        {
            return null;
        }

        string fileType = archivo.ContentType.Split('/')[0];
        UploadResult resultado;

        if (fileType == "image")
        {
            var parametros = new ImageUploadParams
            {
                File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                Folder = carpeta
            };
            resultado = await _cloudinary.UploadAsync(parametros);
        }
        else if (fileType == "video")
        {
            var parametros = new VideoUploadParams
            {
                File = new FileDescription(archivo.FileName, archivo.OpenReadStream()),
                Folder = carpeta
            };
            resultado = await _cloudinary.UploadAsync(parametros);
        }
        else
        {
            return null;
        }

        return resultado.SecureUrl.ToString();
    }
}
