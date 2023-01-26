using AvaliaAe.Helpers;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AvaliaAe.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IUserRepository _repository;
		private readonly IInstitutionRepository _instituionRepository;
		private readonly IInstitutionTypeRepository _typeRepository;
		public RegisterController(IUserRepository repo, IInstitutionRepository institutionRepository, IInstitutionTypeRepository typeRepository)
		{
			_repository = repo;
			_instituionRepository = institutionRepository;
			_typeRepository = typeRepository;
		}
		public IActionResult Index()
		{
			UserTypesAndUserViewModel userTypes = new UserTypesAndUserViewModel();
			userTypes.UserTypes = _repository.GetAllTypes();
			userTypes.UserModel = new UserModel();
			return View(userTypes);
		}

		public IActionResult Institution()
		{
			RegisterInstitutionViewModel viewModel = new RegisterInstitutionViewModel() { institutionType = _typeRepository.getAllInstitutionTypes() };
			return View(viewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}


		[HttpPost]
		public IActionResult CreateNewUser(UserTypesAndUserViewModel user)
		{
			if (ModelState.IsValid)
			{

				if (_repository.GetUserByEmail(user.UserModel.Email) != null)
				{
					TempData["error"] = "Erro: Usuário já existente com este email";
					return RedirectToAction("Index");
				}
				CryptographyHelper crypto = new CryptographyHelper(SHA256.Create());
				user.UserModel.Password = crypto.hashPassword(user.UserModel.Password);
				_repository.InsertUser(user.UserModel);
				TempData["success"] = "Cadastrado com sucesso!";
			}
			else
			{
				var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
			}

			user.UserTypes = _repository.GetAllTypes();
			return View("Index", user);
		}

		[HttpPost]
		public IActionResult CreateNewInstitution(RegisterInstitutionViewModel model)
		{
			try
			{
				if (_instituionRepository.GetInstitutionByEmailAndCnpj(model.institutionModel.Email, model.institutionModel.Cnpj) != null)
				{
					TempData["errorInstitution"] = "Erro: Email ou Cnpj já existente na plataforma";
					return RedirectToAction("Institution");
				}
				CryptographyHelper crypto = new CryptographyHelper(SHA256.Create());
				model.institutionModel.Password = crypto.hashPassword(model.institutionModel.Password);
				_instituionRepository.InsertNewInstitution(model.institutionModel);
				TempData["successInstitution"] = "Instituição cadastrada com sucesso!";
				return RedirectToAction("Institution");

			}
			catch (Exception e)
			{
				return RedirectToAction("Institution");
			}
		}
	}
}
