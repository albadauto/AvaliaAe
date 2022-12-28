using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class ToAssociateController : Controller
    {
        public ToAssociateController()
        {

        }
        public IActionResult Index(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public IActionResult UploadDoc()
        {
            return RedirectToAction("Index");
        }

    }
}
