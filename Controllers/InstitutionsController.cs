using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class InstitutionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertNewInstitution()
        {
            return View();
        }
    }
}
