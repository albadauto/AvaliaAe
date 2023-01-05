using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class MyInstitutionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
