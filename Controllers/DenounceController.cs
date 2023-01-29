using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class DenounceController : Controller
    {
        public IActionResult Index(int Id)
        {
            return View();
        }
    }
}
