using BEARFLIX.Filters;
using BEARFLIX.Models.BD;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BEARFLIX.Controllers
{
    [Authorize(Roles = "USUARIO, TESTER")]
    public class UsuarioController : Controller
    {
        private readonly BearflixContext _context;

        public UsuarioController(BearflixContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var usuario = _context.Usuario.FirstOrDefault(u => u.Id.ToString() == userId);

                if (usuario == null)
                {
                    await HttpContext.SignOutAsync(); 
                    return RedirectToAction("Index", "Inicio");
                }

                return View("Index", usuario);
            }

            return RedirectToAction("Index", "Inicio");
        }
    }
}
