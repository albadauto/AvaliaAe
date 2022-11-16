using AvaliaAe.Helpers;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AvaliaAe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _repository;
        public LoginController(IUserRepository repository)
        {
            _repository = repository;    
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForTest()
        {
            HttpContext.Session.SetString("name", "super");
            return View("Index", "Home");
        }

        [HttpPost]
        public IActionResult VerifyUser(UserModel user)
        {
            CryptographyHelper helper = new CryptographyHelper(SHA256.Create());
            user.Password = helper.hashPassword(user.Password);
            var result = _repository.VerifyLogin(user);
            if(result != null)
            {
				HttpContext.Session.SetString("email", user.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Erro: Usuario ou senha inexistente";
                return RedirectToAction("Index");
            }

		}

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }
    }
}
