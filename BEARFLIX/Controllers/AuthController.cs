using BEARFLIX.Models.DB;
using BEARFLIX.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BEARFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BearflixContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(BearflixContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterDto registerDto)
        {
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo == registerDto.Correo);

            if (usuarioExistente != null)
            {
                return BadRequest("El correo electrónico ya está registrado.");
            }

            // Crear el nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = registerDto.Nombre,
                Correo = registerDto.Correo,
                Contrasena = HashPassword(registerDto.Contrasena),
                FechaNacimiento = registerDto.FechaNacimiento
            };

            // Buscar el rol "USUARIO"
            var rolUsuario = await _context.Rol
                .FirstOrDefaultAsync(r => r.Descripcion == "USUARIO");

            if (rolUsuario != null)
            {
                
                nuevoUsuario.IdRol.Add(rolUsuario);
            }
            else
            {
                return BadRequest("Rol 'USUARIO' no encontrado.");
            }

            // Guardar el usuario con su rol en la base de datos
            _context.Usuario.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente con el rol 'USUARIO'.");
        }


        // Login de usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);

            if (user == null)
            {
                _logger.LogWarning($"El correo {loginDto.Correo} no se encuentra registrado.");
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            if (!VerifyPassword(loginDto.Contrasena, user.Contrasena))
            {
                _logger.LogWarning($"La contraseña ingresada no es correcta para el usuario {loginDto.Correo}.");
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            // Claims de usuario
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Nombre),
        new Claim(ClaimTypes.Email, user.Correo),
    };

            var claimsIdentity = new ClaimsIdentity(claims, "login");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginDto.Recuerdame,
                ExpiresUtc = loginDto.Recuerdame ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddMinutes(30),
            };

            // Iniciar sesión con cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            _logger.LogInformation($"Usuario {user.Nombre} ha iniciado sesión correctamente.");

            // Devuelve un mensaje de éxito
            return Ok(new { message = "Login exitoso" });
        }


        // Logout de usuario
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Inicio"); // Redirige al usuario a la página de inicio después de cerrar sesión
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

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var parts = storedHash.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedPasswordHash = parts[1];

            string hashOfInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashOfInput == storedPasswordHash;
        }
    }
}
