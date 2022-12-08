using AvaliaAe.Helpers;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace AvaliaAe.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly IInstitutionRepository _instituionRepository;
        public RegisterController(IUserRepository repo, IInstitutionRepository institutionRepository)
        {
            _repository = repo;
            _instituionRepository = institutionRepository;
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

		[HttpPost]
		public IActionResult CreateNewInstitution(InstitutionModel model)
		{
			if (ModelState.IsValid)
			{
				CryptographyHelper crypto = new CryptographyHelper(SHA256.Create());
				model.Password = crypto.hashPassword(model.Password);
				_instituionRepository.InsertNewInstitution(model);
				TempData["successInstitution"] = "Instituição cadastrada com sucesso!";
				return RedirectToAction("Institution");
			}
            return View("Institution");
		}
	}
}
