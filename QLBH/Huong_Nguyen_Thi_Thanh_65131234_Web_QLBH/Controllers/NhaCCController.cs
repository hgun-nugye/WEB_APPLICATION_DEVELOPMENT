using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class NhaCCController : Controller
	{
		private readonly AppDbContext _context;

		public NhaCCController(AppDbContext context)
		{
			_context = context;
		}

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

		// READ - Danh sách Nhà cung cấp
		public IActionResult NhaCC_Admin()
		{
			Index();
			var list = _context.NhaCCs.ToList();
			return View(list);
		}

		// DETAILS - Xem chi tiết
		public IActionResult Details(string id)
		{
			var ncc = _context.NhaCCs.FirstOrDefault(x => x.MaNCC == id);
			if (ncc == null)
				return NotFound();
			return View(ncc);
		}

		// CREATE - GET
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		// CREATE - POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NhaCC model)
		{
			if (ModelState.IsValid)
			{
				_context.NhaCCs.Add(model);
				_context.SaveChanges();
				return RedirectToAction("NhaCC_Admin");
			}
			return View(model);
		}

		// EDIT - GET
		[HttpGet]
		public IActionResult Edit(string id)
		{
			var ncc = _context.NhaCCs.FirstOrDefault(x => x.MaNCC == id);
			if (ncc == null)
				return NotFound();
			return View(ncc);
		}

		// EDIT - POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(NhaCC model)
		{
			if (ModelState.IsValid)
			{
				_context.NhaCCs.Update(model);
				_context.SaveChanges();
				TempData["UpdateMessage"] = "Cập nhật thông tin thành công!";
				return RedirectToAction("NhaCC_Admin");
			}
			return View(model);
		}

		// DELETE - GET
		[HttpGet]
		public IActionResult Delete(string id)
		{
			var ncc = _context.NhaCCs.FirstOrDefault(x => x.MaNCC == id);
			if (ncc == null)
				return NotFound();
			return View(ncc);
		}

		// DELETE - POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			var ncc = _context.NhaCCs.FirstOrDefault(x => x.MaNCC == id);
			if (ncc != null)
			{
				_context.NhaCCs.Remove(ncc);
				_context.SaveChanges();
				TempData["SuccessMessage"] = "Đã xóa nhà cung cấp thành công!";
			}
			else
			{
				TempData["ErrorMessage"] = "Không tìm thấy nhà cung cấp cần xóa!";
			}

			return RedirectToAction("NhaCC_Admin");
		}

	}
}
