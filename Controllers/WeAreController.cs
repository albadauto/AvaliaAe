using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class WeAreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
