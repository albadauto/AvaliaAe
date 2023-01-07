using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class MyInstitutionsController : Controller
    {
        private readonly IAssociationRepository _associationRepository;
        public MyInstitutionsController(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }
        public IActionResult Index(int Id)
        {
            int idUser = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
            if(Id == null || Id == 0 || idUser != Id )
            {
                return RedirectToAction("Index", "Home");
            }
           var result = _associationRepository.GetUserAndInstitution(Id);
            return View(result);
        }

        public IActionResult ToAvaliate(int Id)
        {

            return View();
        }
    }
}
