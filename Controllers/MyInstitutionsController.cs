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
        private readonly IAverageRepository _averageRepository;
        public MyInstitutionsController(IAssociationRepository associationRepository, IAvaliationRepository avaliationRepository, IInstitutionRepository institutionRepository, ICalculationHelper calculation, IAverageRepository averageRepository)
        {
            _associationRepository = associationRepository;
            _avaliationRepository = avaliationRepository;
            _institutionRepository = institutionRepository;
            _calculationHelper = calculation;
            _averageRepository = averageRepository;
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
                avaliate.AvaliationModel.Average = average;

            }


            avaliate.InstitutionName = institution?.InstitutionName ?? "Minha instituição";
            return View(avaliate);
        }

        //Função para inserir ou dar update em uma média
        private void insertOrUpdateAverage(List<int> notesList, AvaliateViewModel model)
        {
            if (notesList.Count > 0)
            {
                double average = _calculationHelper.CalculateAverage(notesList);
                var averagesearch = _averageRepository.getAverageByIdInstitution(model.AvaliationModel.InstitutionId);
                if (averagesearch != null) //Caso aja registro de média fazer um update
                {
                    _averageRepository.updateAverage(new AverageModel()
                    {
                        Average = average,
                        InstitutionId = model.AvaliationModel.InstitutionId
                    });
                }
                else //Se não, inserir do zero.
                {
                    _averageRepository.insertAverage(new AverageModel()
                    {
                        Average = average,
                        InstitutionId = model.AvaliationModel.InstitutionId
                    });
                }
            }
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

                var allnotes = _avaliationRepository.GetAllComments(model.AvaliationModel.InstitutionId);

                //Verifica se o objeto está nulo
                if (model.AvaliationModel != null)
                {
                    model.AvaliationModel = new AvaliationModel() { InstitutionId = model.AvaliationModel.InstitutionId }; //INSERE o ID da instituição no novo objeto 
                }
                List<int> notesList = new List<int>(); //Cria nova lista de notas
                foreach (var i in allnotes)
                {
                    notesList.Add(i.Note); //Adiciona as notas no objeto criado
                }

                insertOrUpdateAverage(notesList, model); //Adiciona a média no banco de dados

                TempData["successAvaliation"] = "Avaliação feita com sucesso, agradecemos a contribuição";
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }


    }
}
