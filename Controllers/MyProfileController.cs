using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUserRepository _repository;

        public MyProfileController(IUserRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index() 
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(int id)
        {
            if (HttpContext.Session.GetInt32("Id") != id)
            {
                return RedirectToAction("Index", "Login");
            }
            var all = _repository.GetUser(id);
            return View(all);
        }
    }
}
