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
        private readonly IInstitutionRepository _institutionRepository;
        public LoginController(IUserRepository repository, IInstitutionRepository institution)
        {
            _repository = repository;    
            _institutionRepository = institution;
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
                HttpContext.Session.SetString("type", "user");
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
            FormatHelper format = new FormatHelper();
            if(institution.Password == null)
            {
                return RedirectToAction("Index");
            }
            institution.Password = helper.hashPassword(institution.Password);
            var result = _institutionRepository.GetInstitutionByEmailAndPassword(format.FormatCPF(institution.Cnpj), institution.Password);
            if(result != null ) {
				HttpContext.Session.SetString("email", result.Email);
				HttpContext.Session.SetInt32("Id", result.Id);
                HttpContext.Session.SetString("type", "inst");
                return RedirectToAction("Index", "Home");
            }
            else
            {
				TempData["error"] = "Erro: Instituição com senha ou CNPJ inválidos";
				return RedirectToAction("Index");
			}
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("email");
            if (HttpContext.Session.GetString("Type") != null)
            {
                HttpContext.Session.Remove("Type");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
