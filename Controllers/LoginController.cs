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
            if (user.Password == null)
            {
                return RedirectToAction("Index");
            }


            user.Password = helper.hashPassword(user.Password);
            var result = _repository.VerifyLogin(user);
            if(result != null)
            {
				HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetInt32("Id", result.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Erro: Usuario ou senha inexistente";
                return RedirectToAction("Index");
            }

		}


        [HttpPost]
        public IActionResult VerifyInstitution(InstitutionModel institution)
        {
            CryptographyHelper helper = new CryptographyHelper(SHA256.Create());
            return RedirectToAction();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }
    }
}
