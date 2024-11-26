using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace BEARFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public FileController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpPost("cargar")]
        public async Task<IActionResult> CargarArchivo(IFormFile file, [FromQuery] string folder)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Sin archivo.");
            }

            string fileType = file.ContentType.Split('/')[0]; 

            UploadResult cargaResultado;

            if (fileType == "image")
            {
                var cargaParametros = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = folder
                };

                cargaResultado = await _cloudinary.UploadAsync(cargaParametros);
            }
            else if (fileType == "video")
            {
                var cargaParametros = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = folder
                };

                cargaResultado = await _cloudinary.UploadAsync(cargaParametros);
            }
            else
            {
                return BadRequest("Tipo de archivo no soportado. Solo se permiten imágenes y videos.");
            }

            if (cargaResultado.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(cargaResultado.SecureUrl.ToString());
            }

            return StatusCode(500, "Error al cargar el archivo en Cloudinary.");
        }
    }
}
