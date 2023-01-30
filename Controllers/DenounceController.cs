using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class DenounceController : Controller
    {
        public readonly IDenounceRepository _repository;
        public DenounceController(IDenounceRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index(int Id)
        {
            DenouncesModel denounce = new DenouncesModel() { AvaliationId = Id };
            return View(denounce);
        }

        [HttpPost]
        public IActionResult DoDenounce(DenouncesModel model)
        {
            try 
            {
                _repository.InsertDenounce(new DenouncesModel() { Comment = model.Comment, AvaliationId = model.AvaliationId });
                TempData["successAvaliation"] = "Denúncia feita com sucesso";
                return RedirectToAction("ToAvaliate", "MyInstitutions", new { Id = HttpContext.Session.GetInt32("Id") });
            }
            catch (Exception e)
            {
                TempData["errorAvaliation"] = "Erro: Contate um administrador. Erro original: " + e.Message;
                return RedirectToAction("MyInstitutions", "ToAvaliate", new { Id = Convert.ToInt32(HttpContext.Session.GetInt32("Id")) });
            }
        }
    }
}
