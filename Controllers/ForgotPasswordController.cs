using Microsoft.AspNetCore.Mvc;

namespace AvaliaAe.Controllers
{
	public class ForgotPasswordController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
