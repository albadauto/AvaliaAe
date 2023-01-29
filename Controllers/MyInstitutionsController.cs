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
        private readonly IInstitutionRepository _institutionRepository;
        public MyInstitutionsController(IAssociationRepository associationRepository, IAvaliationRepository avaliationRepository, IInstitutionRepository institutionRepository)
        {
            _associationRepository = associationRepository;
            _avaliationRepository = avaliationRepository;
            _institutionRepository = institutionRepository;
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
            AvaliateViewModel avaliate = new AvaliateViewModel();
            int? idUser = HttpContext.Session.GetInt32("Id");
            if (idUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var allInstitutionById = _avaliationRepository.GetAllComments(Id);
            var institutionName = _institutionRepository.GetInstitutionById(Id);
            avaliate.Avaliations = allInstitutionById;
            avaliate.InstitutionName = institutionName.InstitutionName;
            return View(avaliate);
        }


        [HttpPost]
        public IActionResult RegisterAvaliation(AvaliateViewModel model)
        {
            try
            {
                if(model.AvaliationModel.Comment == null || model.AvaliationModel.Note == null)
                {
                    TempData["errorAvaliation"] = "Insira todas as informações necessárias para a avaliação.";
                    return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
                }
                model.AvaliationModel.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                _avaliationRepository.InsertAvaliation(model.AvaliationModel);
                TempData["successAvaliation"] = "Avaliação feita com sucesso, agradecemos a contribuição";
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }
            catch(Exception e)
            {
                TempData["errorAvaliation"] = "Erro: Contate um administrador. Erro original: " + e.Message;
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }
          

        }


    }
}
