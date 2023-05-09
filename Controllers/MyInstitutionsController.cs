using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using AvaliaAe.Repository;
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

            if (avaliate.AvaliationModel.Average == null)
            {
                avaliate.AvaliationModel.Average = new AverageModel();
            }

            var average = (_averageRepository.getAverageByIdInstitution(Id) == null) ? (double?)0.0 : _averageRepository.getAverageByIdInstitution(Id).Average;

            avaliate.AvaliationModel.Average.Average = average;
            


            avaliate.InstitutionName = institution?.InstitutionName ?? "Minha instituição";
            return View(avaliate);
        }

        //Função para inserir ou dar update em uma média
        private void insertOrUpdateAverage(List<int> notesList, int InstitutionId)
        {
            double average = _calculationHelper.CalculateAverage(notesList);
            var averagesearch = _averageRepository.getAverageByIdInstitution(InstitutionId);
            if (averagesearch != null) //Caso aja registro de média fazer um update
            {
                _averageRepository.updateAverage(new AverageModel()
                {
                    Average = average,
                    InstitutionId = InstitutionId,
                });
            }
            else //Se não, inserir do zero.
            {
                _averageRepository.insertAverage(new AverageModel()
                {
                    Average = average,
                    InstitutionId = InstitutionId,
                });
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

                var averageByIdInst = _averageRepository.getAverageByIdInstitution(model.AvaliationModel.InstitutionId);

                if (_avaliationRepository.GetAvaliationByUserId((int)HttpContext.Session.GetInt32("Id"), model.AvaliationModel.InstitutionId) != null)
                {
                    Console.WriteLine(model.AvaliationModel.InstitutionId);
                    TempData["errorAvaliation"] = "Você já avaliou esta instituição";
                    return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
                }

                //------------------------------------------------------------------
                model.AvaliationModel.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("Id"));
                var allnotes = _avaliationRepository.GetAllComments(model.AvaliationModel.InstitutionId);
                List<int> notesList = new List<int>(); //Cria nova lista de notas

                if (allnotes.Count > 0)
                {
                    foreach (var i in allnotes)
                    {
                        notesList.Add(model.AvaliationModel.Note);
                        notesList.Add(i.Note); //Adiciona as notas no objeto criado
                    }
                }
                else
                {
                    notesList.Add(model.AvaliationModel.Note);
                }
                
                insertOrUpdateAverage(notesList, model.AvaliationModel.InstitutionId);
                var averageIdSearched = _averageRepository.getAverageByIdInstitution(model.AvaliationModel.InstitutionId);

                model.AvaliationModel.AverageId = averageIdSearched.Id;

                _avaliationRepository.InsertAvaliation(model.AvaliationModel);

                //Verifica se o objeto está nulos
                if (model.AvaliationModel != null)
                {
                    model.AvaliationModel = new AvaliationModel() { InstitutionId = model.AvaliationModel.InstitutionId }; //INSERE o ID da instituição no novo objeto 
                }

                TempData["successAvaliation"] = "Avaliação feita com sucesso, agradecemos a contribuição";
                return RedirectToAction("ToAvaliate", new { Id = model.AvaliationModel.InstitutionId });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        [HttpGet]
        public IActionResult RemoveAvaliation(int idInst, int userId)
        {
            try
            {
                var result = _avaliationRepository.RemoveAvaliationAverage(idInst, userId);
                var allRates = _avaliationRepository.GetAllComments(idInst); 
                List<int> notes = new();
                foreach (var rate in allRates)
                {
                    notes.Add(rate.Note);
                }
                insertOrUpdateAverage(notes, idInst);
                if (result)
                {
                    TempData["successAvaliation"] = "Comentário removido com sucesso";

                }
                else
                {
                    TempData["errorAvaliation"] = "Erro: Contatar um administrador";
                }
                return RedirectToAction("ToAvaliate", new { Id = idInst });

            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

    }
}
