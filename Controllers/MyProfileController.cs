using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInstitutionRepository _institutionRepository;

        public MyProfileController(IUserRepository repository, IInstitutionRepository institutionRepository)
        {
            _repository = repository;
            _institutionRepository = institutionRepository; 
        }

        public ActionResult Index() 
        {
      
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(int id)
        {
            if (HttpContext.Session.GetString("type") != "user" || HttpContext.Session.GetString("type") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetInt32("Id") != id)
            {
                return RedirectToAction("Index", "Login");
            }
			UserModel result = _repository.GetUser(id);
			return View(result);
        }

        public IActionResult InstitutionProfile(int id)
        {
            if (HttpContext.Session.GetString("type") != "inst" || HttpContext.Session.GetString("type") == null || HttpContext.Session.GetInt32("Id") != id)
            {
                return RedirectToAction("Index", "Home");
            }

         
            InstitutionModel inst = _institutionRepository.GetInstitutionById(id);
            if (inst == null) {
                return RedirectToAction("Index", "Home");
            }
     
            return View(inst);
        }
    }
}
