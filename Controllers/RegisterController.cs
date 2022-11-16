using AvaliaAe.Helpers;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace AvaliaAe.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _repository;
        public RegisterController(IUserRepository repo)
        {
            _repository= repo;  
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Institution()
        {
            return View();
        }

       public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateNewUser(UserModel user)
        {
            if(ModelState.IsValid)
            {
                CryptographyHelper crypto = new CryptographyHelper(SHA256.Create());
                user.Password = crypto.hashPassword(user.Password);
				_repository.InsertUser(user);
                TempData["success"] = "Cadastrado com sucesso!";
				return RedirectToAction("Index");
			}
            return View("Index", user);
        }
    }
}
