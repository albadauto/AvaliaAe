using AvaliaAe.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
	public class ForgotPasswordController : Controller
	{
		private readonly IEmail _mail;

		public ForgotPasswordController(IEmail mail)
		{
			_mail = mail; 
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
	}
}
