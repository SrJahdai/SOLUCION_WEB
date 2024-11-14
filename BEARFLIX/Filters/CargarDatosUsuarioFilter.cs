using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using BEARFLIX.Models.BD;

namespace BEARFLIX.Filters
{
    public class CargarDatosUsuarioFilter : IActionFilter
    {
        private readonly BearflixContext _context;

        public CargarDatosUsuarioFilter(BearflixContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var userId = controller?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var usuario = _context.Usuario
                    .Where(u => u.Id.ToString() == userId)
                    .Select(u => new
                    {
                        u.Nombre,
                        u.Foto,
                        RolDescripcion = u.IdRol.Select(rol => rol.Descripcion).FirstOrDefault()
                    })
                    .FirstOrDefault();

                if (usuario != null)
                {
                    controller.ViewData["NombreUsuario"] = usuario.Nombre;
                    controller.ViewData["FotoPerfil"] = usuario.Foto;
                    controller.ViewData["RolUsuario"] = usuario.RolDescripcion;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
