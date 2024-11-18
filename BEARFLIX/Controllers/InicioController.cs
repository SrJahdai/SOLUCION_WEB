using BEARFLIX.Models;
using BEARFLIX.Models.BD;  // Agregar para usar BearflixContext
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;  // Agregar para usar ClaimTypes

namespace BEARFLIX.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly BearflixContext _context;  // Agregar el contexto de la base de datos

        // Inyección de dependencias para acceder a BearflixContext
        public InicioController(ILogger<InicioController> logger, BearflixContext context)
        {
            _logger = logger;
            _context = context;  // Inicialización del contexto
        }

        // En InicioController
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Obtener el rol del usuario desde los claims
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (userRole == "ADMINISTRADOR" || userRole == "DUENO")
                {
                    return RedirectToAction("Index", "Peliculas");
                }
                else if (userRole == "USUARIO")
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return RedirectToAction("Index", "Usuario", new { id = userId });
                }
            }

            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }




        // Métodos adicionales para otras páginas
        public IActionResult Pruebas()
        {
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }

        public IActionResult Dudas()
        {
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }

        public IActionResult Politicas()
        {
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }

        public IActionResult Contactanos()
        {
            ViewData["Layout"] = "~/Views/Shared/_Layout.cshtml";
            return View();
        }
    }
}
