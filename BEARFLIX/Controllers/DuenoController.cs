using BEARFLIX.Models.BD;
using BEARFLIX.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BEARFLIX.Controllers
{
    public class DuenoController : Controller
    {
        private readonly BearflixContext _context;

        public DuenoController(BearflixContext context)
        {
            _context = context;
        }

        // Vista principal de Dueño
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GestionUsuario()
        {
            var roles = await _context.Rol.ToListAsync();
            var usuarios = await _context.Usuario
                .Include(u => u.IdRol)
                .Select(u => new
                {
                    u.Nombre,
                    u.Correo,
                    Rol = u.IdRol.FirstOrDefault().Descripcion,
                    u.FechaNacimiento
                })
                .ToListAsync();

            ViewData["Roles"] = roles;
            ViewBag.Usuarios = usuarios;

            return View();
        }


        // Registrar usuario
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(RegisterDto registerDto, int idRol)
        {
            // Verificar si el correo ya está registrado
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo == registerDto.Correo);

            if (usuarioExistente != null)
            {
                TempData["Error"] = "El correo electrónico ya está registrado.";
                return RedirectToAction("GestionUsuario");
            }

            // Crear el nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Correo = registerDto.Correo,
                Contrasena = HashPassword(registerDto.Contrasena),
                FechaNacimiento = registerDto.FechaNacimiento
            };

            // Asignar el rol seleccionado
            var rol = await _context.Rol.FirstOrDefaultAsync(r => r.Id == idRol);
            if (rol == null)
            {
                TempData["Error"] = "Rol no encontrado.";
                return RedirectToAction("GestionUsuario");
            }

            nuevoUsuario.IdRol.Add(rol);

            // Guardar el usuario con su rol en la base de datos
            _context.Usuario.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario registrado correctamente.";
            return RedirectToAction("GestionUsuario");
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
    }
}
