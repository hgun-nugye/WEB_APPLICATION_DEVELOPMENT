using Microsoft.AspNetCore.Mvc;

namespace huongntt65131234_BaiTap0.Controllers
{
	public class BaiTap0Controller : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult IndexHomeUD2()
		{
			ViewBag.AString = "Hello World from ViewBag";
			return View();
		}

		public IActionResult IndexHomeUD3()
		{
			var message = new Models.MessageModel();
			message.Welcome = "Hello World from Model";
			return View(message);
		}
	}
}
