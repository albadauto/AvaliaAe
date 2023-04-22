using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class SearchCertificationController : Controller
    {
        private readonly ICertificationRepository _repository;

        public SearchCertificationController(ICertificationRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Certificate(string CodeCertification)
        {
            try
            {
                var result = _repository.GetCertificationByCode(CodeCertification);
                if(result != null)
                {
                    return View(result);
                }
                else
                {
                    TempData["errorSearch"] = "Código inexistente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
