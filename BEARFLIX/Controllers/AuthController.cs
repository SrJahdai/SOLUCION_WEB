using BEARFLIX.Models.DB;
using BEARFLIX.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BEARFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BearflixContext _context;

        public AuthController(BearflixContext context)
        {
            _context = context;
        }

        // Registro de usuario
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterDto registerDto)
        {
            // Verificar si el correo electrónico ya existe
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo == registerDto.Correo);

            if (usuarioExistente != null)
            {
                return BadRequest("El correo electrónico ya está registrado.");
            }

            // Aquí va el código para crear el nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Correo = registerDto.Correo,
                Contrasena = registerDto.Contrasena,
                FechaNacimiento = registerDto.FechaNacimiento
            };

            _context.Usuario.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente.");
        }


        // Login de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);

            if (user == null)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            // Verificar la contraseña
            if (!VerifyPassword(loginDto.Contrasena, user.Contrasena))
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            return Ok("Login exitoso.");
        }

        // Método para hash de la contraseña sin usar salt
        private string HashPassword(string password)
        {
            // Generamos el hash directamente sin salt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[0], // Sin salt
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        // Método para verificar la contraseña
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashOfInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: new byte[0], // Sin salt
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashOfInput == storedHash;
        }
    }
}
