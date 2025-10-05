using Huong_Nguyen_Thi_Thanh___65131234___Bai_tap_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huong_Nguyen_Thi_Thanh___65131234___Bai_tap_1.Controllers
{
	public class ThamSoActionController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.tg = new
			{
				HoTen = "Nguyễn Thị Thanh Hương",
				MSSV = "65131234",
				DienThoai = "0935724503",
			};
			return View();
		}

		// 3.2. myRequest
		// GET
		[HttpGet]
		public IActionResult myRequest()
		{
			Index();
			return View();
		}

		// POST
		[HttpPost]
		public IActionResult myRequest(int a, int b)
		{
			ViewBag.A = a;
			ViewBag.B = b;
			ViewBag.Tong = a + b;

			Index();
			return View();
		}

		// 3.3. myArgument
		public ActionResult myArgument(int a = 0, int b = 0)
		{
			Index();
			// Gán giá trị cho ViewBag
			ViewBag.A = a;
			ViewBag.B = b;
			ViewBag.Tong = a + b;
			return View();
		}

		// 3.4. myFormCollection

		[HttpGet]
		public ActionResult myFormCollection()
		{
			Index();
			return View();
		}

		[HttpPost]
		public ActionResult myFormCollection(IFormCollection f)
		{
			Index();

			// Kiểm tra và lấy giá trị từ FormCollection
			if (f.ContainsKey("a") && f.ContainsKey("b") &&
				!string.IsNullOrEmpty(f["a"].ToString()) && !string.IsNullOrEmpty(f["b"].ToString()))
			{
				int a = 0, b = 0;
				int.TryParse(f["a"].ToString(), out a);
				int.TryParse(f["b"].ToString(), out b);

				ViewBag.A = a;
				ViewBag.B = b;
				ViewBag.Tong = a + b;
			}

			return View();
		}

		// 3.5. myModel
		[HttpGet]
		public ActionResult myModel()
		{
			Index();

			return View();
		}

		[HttpPost]
		public ActionResult myModel(TongCls model)
		{
			Index();

			if (model != null)
			{
				ViewBag.Tong = model.tong();
			}

			return View(model);
		}
	}
}


