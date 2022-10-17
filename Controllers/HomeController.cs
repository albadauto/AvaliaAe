using AvaliaAe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AvaliaAe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}