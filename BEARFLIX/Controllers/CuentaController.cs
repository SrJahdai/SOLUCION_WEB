using Microsoft.AspNetCore.Mvc;

namespace BEARFLIX.Controllers
{
    public class CuentaController : Controller
    {
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Register()
        {
            return View("Register");
        }
    }
}
