using AvaliaAe.Helpers;
using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AvaliaAe.Controllers
{
	public class ForgotPasswordController : Controller
	{
		private readonly IEmail _mail;
		private readonly IUserRepository _repository;
		private readonly ICodeRepository _codeRepository;
		public ForgotPasswordController(IEmail mail, IUserRepository repository, ICodeRepository codeRepository)
		{
			_mail = mail; 
			_repository = repository;
			_codeRepository = codeRepository;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CompleteVerification()
		{

            return View();
		}

		[HttpPost]
		public IActionResult VerifyEmail(UserModel model) 
		{
			var result = _repository.VerifyIfEmailIsValid(model.Email);
			ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel();
            foreach (var value in result)
            {
				viewModel.Email = value.Email;
				viewModel.Id = value.Id;
				Console.WriteLine(value.Id);
            }


			if (viewModel.Email != null)
			{
				Random rand = new Random();
				var cod = rand.Next().ToString().Substring(0, 5);
				_mail.SendMail(model.Email, "(Não compartilhe com ninguém!) Recuperação de senha - Avalia Aê!", $"Utilize o código: {cod} para recuperar a senha");
				_codeRepository.InsertNewCode(new CodeModel { Code = cod, UserModelId = viewModel.Id });
				HttpContext.Session.SetInt32("idUser", viewModel.Id);
				return RedirectToAction("CompleteVerification");

			}
			else {
				TempData["errorMail"] = "Email inexistente";
				return RedirectToAction("Index");
			
			}
			
		}
		[HttpPost]
		public IActionResult CreateNewPassword(CreatePasswordViewModel newUser)
		{
			var findedCode = _codeRepository.FindCode(newUser.Code);
			CryptographyHelper crypt = new CryptographyHelper(SHA256.Create());

            if (findedCode != null)
			{

                int idUser = HttpContext.Session.GetInt32("idUser") ?? 0;
                _repository.ResetPassword(new UserModel() { Id = idUser, Password = crypt.hashPassword(newUser.User.Password) });
				TempData["successEdit"] = "Senha atualizada com sucesso!";
                HttpContext.Session.Remove("idUser");
                return RedirectToAction("CompleteVerification");
			}
			else
			{
				TempData["errorEdit"] = "Erro: Código inexistente!";
                return RedirectToAction("CompleteVerification");

            }
        }
	}
}
