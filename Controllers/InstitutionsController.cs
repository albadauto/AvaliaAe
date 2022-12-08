using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly IInstitutionRepository _repository;
        public InstitutionsController(IInstitutionRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertNewInstitution()
        {
            return View();
        }

    }
}
