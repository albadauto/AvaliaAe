using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AvaliaAe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAverageRepository _averageRepository;
        public HomeController(IAverageRepository average)
        {
            _averageRepository = average;
        }
        public IActionResult Index()
        {
            var result = _averageRepository.getAllAverage();
            return View(result);
        }

    }
}