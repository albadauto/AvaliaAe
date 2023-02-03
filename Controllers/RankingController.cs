using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class RankingController : Controller
    {
        private readonly IAvaliationRepository _avaliationRepository;
        public RankingController(IAvaliationRepository avaliationRepository)
        {
            _avaliationRepository = avaliationRepository;
        }
        public IActionResult Index()
        {
            var result = _avaliationRepository.allAvaliations();
            return View(result);
        }
    }
}
