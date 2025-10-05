using Microsoft.AspNetCore.Mvc;

namespace huongntt65131234_BaiTap1.Controllers
{
	public class CalculatorController : Controller
	{
		public ActionResult Index(double a, double b, string pt = "+")
		{
			switch (pt)
			{
				case "+": ViewBag.KQ = a + b; break;
				case "-": ViewBag.KQ = a - b; break;
				case "*": ViewBag.KQ = a * b; break;
				case "/":
					if (b == 0) ViewBag.KQ = "Không chia được cho 0"; else ViewBag.KQ = a / b; break;
			}
			return View();
		}
	}
}
