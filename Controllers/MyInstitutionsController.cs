using AvaliaAe.Helpers.Interfaces;
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
        private readonly ICalculationHelper _calculationHelper;
        private int sum = 0;
        private int Average = 0;
        public MyInstitutionsController(IAssociationRepository associationRepository, IAvaliationRepository avaliationRepository, IInstitutionRepository institutionRepository, ICalculationHelper calculation)
        {
            _associationRepository = associationRepository;
            _avaliationRepository = avaliationRepository;
            _institutionRepository = institutionRepository;
            _calculationHelper = calculation;
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
            if (HttpContext.Session.GetInt32("Id") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            AvaliateViewModel avaliate = new AvaliateViewModel();
            List<int> notes = new List<int>();
            var comments = _avaliationRepository.GetAllComments(Id);
            var institution = _institutionRepository.GetInstitutionById(Id);
            avaliate.Avaliations = comments;

            if (avaliate.AvaliationModel == null)
            {
                avaliate.AvaliationModel = new AvaliationModel();
            }

            foreach (var i in comments)
            {
                notes.Add(i.Note);

            }

            if (notes.Count > 0)
            {
                double average = _calculationHelper.CalculateAverage(notes);
                avaliate.AvaliationModel.Average = 20;
                if (avaliate.AvaliationModel.Average != null)
                {
                    Console.WriteLine("Não é nulo");
                }
                else
                {
                    Console.WriteLine("é nulo");

                }
            }


            avaliate.InstitutionName = institution?.InstitutionName ?? "Minha instituição";
            return View(avaliate);
        }


        [HttpPost]
        public IActionResult RegisterAvaliation(AvaliateViewModel model)
        {
            try
            {
                if (model.AvaliationModel.Comment == null || model.AvaliationModel.Note == null)
                {
                    TempData["errorAvaliation"] = "Insira todas as informações necessárias para a avaliação.";
                    return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
                }
                model.AvaliationModel.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                _avaliationRepository.InsertAvaliation(model.AvaliationModel);
                TempData["successAvaliation"] = "Avaliação feita com sucesso, agradecemos a contribuição";
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }
            catch (Exception e)
            {
                TempData["errorAvaliation"] = "Erro: Contate um administrador. Erro original: " + e.Message;
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }


        }


    }
}
