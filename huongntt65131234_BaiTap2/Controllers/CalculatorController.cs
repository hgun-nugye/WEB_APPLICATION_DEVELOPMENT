using huongntt65131234_BaiTap2.Models;
using Microsoft.AspNetCore.Mvc;

namespace huongntt65131234_BaiTap2.Controllers
{
	public class CalculatorController : Controller
	{
		public ActionResult Index(Calculatorcs cal)
		{
			switch (cal.pt)
			{
				case "+": ViewBag.KQ = cal.a + cal.b; break;
				case "-": ViewBag.KQ = cal.a - cal.b; break;
				case "*": ViewBag.KQ = cal.a * cal.b; break;
				case "/":
					if (cal.b == 0) ViewBag.KQ = "Không chia được cho 0"; else ViewBag.KQ = cal.a / cal.b; break;
			}
			return View();
		}
	}
}
