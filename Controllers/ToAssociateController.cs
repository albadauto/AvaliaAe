using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class ToAssociateController : Controller
    {
        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

    }
}
