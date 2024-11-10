using Microsoft.AspNetCore.Mvc;

namespace BEARFLIX.Controllers
{
    public class CuentaController : Controller
    {
        public IActionResult Login()
        {
            ViewData["Layout"] = "~/Views/Shared/_logLayout.cshtml";
            return View("Login");
        }
        public IActionResult Register()
        {
            ViewData["Layout"] = "~/Views/Shared/_logLayout.cshtml";
            return View("Register");
        }
    }
}
