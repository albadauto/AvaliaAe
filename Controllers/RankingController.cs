using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
    public class RankingController : Controller
    {
        private readonly IAverageRepository _averageRepository;
        public RankingController(IAverageRepository averageRepository)
        {
            _averageRepository = averageRepository;
        }
        public IActionResult Index()
        {
            var result = _averageRepository.getAllAverage();
            return View(result);
        }
    }
}
