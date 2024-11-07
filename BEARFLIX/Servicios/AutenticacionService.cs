using BEARFLIX.Models;
using BEARFLIX.Models.BD;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BEARFLIX.Servicios
{
    public class AutenticacionService : IAutenticacion
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AutenticacionService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Usuario> AutenticarUsuario(string correo, string contrasena)
        {
            var usuario = await _userManager.FindByEmailAsync(correo);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            var resultado = await _signInManager.PasswordSignInAsync(usuario, contrasena, false, false);
            if (!resultado.Succeeded)
                throw new Exception("Contraseña incorrecta");

            return usuario;
        }
    }
}
