using BEARFLIX.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BEARFLIX.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;

        public InicioController(ILogger<InicioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pruebas()
        {
            return View();
        }

        public IActionResult Dudas()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Politicas()
        {
            return View();
        }

        public IActionResult Contactanos()
        {
            return View();
        }
    }
}
