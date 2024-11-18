using BEARFLIX.Models.BD;
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


        // Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Usuario
                .Include(u => u.IdRol) // Incluir los roles
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);

            if (user == null || !VerifyPassword(loginDto.Contrasena, user.Contrasena))
            {
                _logger.LogWarning($"Credenciales incorrectas para el correo {loginDto.Correo}.");
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            var rol = user.IdRol.FirstOrDefault()?.Descripcion;
            if (string.IsNullOrEmpty(rol))
            {
                _logger.LogWarning($"El usuario {loginDto.Correo} no tiene un rol asignado.");
                return Unauthorized("No tienes acceso al sistema.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Correo),
                new Claim(ClaimTypes.Role, rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = loginDto.Recuerdame,
                ExpiresUtc = loginDto.Recuerdame ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddMinutes(30),
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            _logger.LogInformation($"Usuario {user.Nombre} ha iniciado sesión correctamente con el rol {rol}.");

            string redirectUrl = GetRedirectUrlByRole(rol);
            return new JsonResult(new { redirectUrl });
        }

        private string GetRedirectUrlByRole(string rol)
        {
            return rol switch
            {
                "USUARIO" => Url.Action("Index", "Usuario"),
                "TESTER" => Url.Action("Index", "Tester"),
                "DUENO" => Url.Action("Index", "Peliculas"),
                "ADMINISTRADOR" => Url.Action("Panel", "Administrador"),
                _ => Url.Action("Index", "Inicio")
            };
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
