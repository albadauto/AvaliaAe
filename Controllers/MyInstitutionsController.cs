using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AvaliaAe.Controllers
{
    public class MyInstitutionsController : Controller
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IAvaliationRepository _avaliationRepository;
        public MyInstitutionsController(IAssociationRepository associationRepository, IAvaliationRepository avaliationRepository)
        {
            _associationRepository = associationRepository;
            _avaliationRepository = avaliationRepository;
        }
        public IActionResult Index(int Id)
        {
            int idUser = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            if (Id == null || Id == 0 || idUser != Id)
            {
                return RedirectToAction("Index", "Login");
            }
            if (HttpContext.Session.GetString("type") == "inst") return RedirectToAction("Index", "Home");
            var result = _associationRepository.GetUserAndInstitution(Id);
            return View(result);
        }

        public IActionResult ToAvaliate(int Id)
        {
            int? idUser = HttpContext.Session.GetInt32("Id");
            if (idUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public IActionResult RegisterAvaliation(AvaliationModel model)
        {

            model.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            model.InstitutionId = 1;
            _avaliationRepository.InsertAvaliation(model);
            return RedirectToAction("Index", "Home");

        }



        /*    private IActionResult VerifySession(string route)
    {
        int idUser = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
        if (idUser == null)
        {
            return RedirectToAction(route);
        }
    }*/
    }
}
