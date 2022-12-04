using AvaliaAe.Helpers;
using AvaliaAe.Helpers.Interfaces;
using AvaliaAe.Models;
using AvaliaAe.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
	public class ForgotPasswordController : Controller
	{
		private readonly IEmail _mail;
		private readonly IUserRepository _repository;

		public ForgotPasswordController(IEmail mail, IUserRepository repository)
		{
			_mail = mail; 
			_repository = repository;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ForTest()
		{
			_mail.SendMail("joseadauto923@gmail.com", "teste", "teste");
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public IActionResult VerifyEmail(UserModel model) 
		{
			var result = _repository.VerifyIfEmailIsValid(model.Email);
			if(result != null)
			{
				Random rand = new Random();
				var cod = rand.Next().ToString().Substring(0, 5);
				_mail.SendMail(model.Email, "(Não compartilhe com ninguém!) Recuperação de senha - Avalia Aê!", $"Utilize o código: {cod} para recuperar a senha");
				return RedirectToAction("Index");

			}
			return RedirectToAction("Index");
		}
	}
}
