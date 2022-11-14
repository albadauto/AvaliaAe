using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForTest()
        {
            HttpContext.Session.SetString("name", "super");
            return View("Index", "Home");
        }
    }
}
