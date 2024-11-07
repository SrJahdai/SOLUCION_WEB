using BEARFLIX.Models.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BEARFLIX.Servicios
{
    public class Autenticacion : IAutenticacion
    {
        private readonly BearflixContext _context;

        public Autenticacion(BearflixContext context)
        {
            _context = context;
        }

        // Obtener un usuario por su ID
        public async Task<Usuario?> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }

        // Registrar un nuevo usuario
        public async Task<Usuario> RegistrarUsuario(Usuario usuario)
        {
            if (_context.Usuario.Any(u => u.Correo == usuario.Correo))
            {
                throw new Exception("El usuario ya existe con ese Correo.");
            }

            usuario.Contrasena = new PasswordHasher().HashPassword(usuario.Contrasena);


            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Actualizar la información de un usuario
        public async Task<Usuario?> ActualizarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                return null; // No se encuentra el usuario
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Correo = usuario.Correo;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;

            // Aquí puedes agregar más campos a actualizar
            _context.Usuario.Update(usuarioExistente);
            await _context.SaveChangesAsync();

            return usuarioExistente;
        }

        // Eliminar un usuario
        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return false; // No se encuentra el usuario
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        // Autenticar un usuario (por ejemplo, validar credenciales)
        public async Task<Usuario> AutenticarUsuario(string Correo, string contrasena)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == Correo);
            if (usuario == null || !new PasswordHasher().VerifyHashedPassword(usuario.Contrasena, contrasena))
            {
                throw new Exception("Credenciales incorrectas.");
            }

            return usuario;
        }

        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        // Add any other claims here like roles or permissions
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
