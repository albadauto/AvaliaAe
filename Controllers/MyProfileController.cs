using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class MyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
