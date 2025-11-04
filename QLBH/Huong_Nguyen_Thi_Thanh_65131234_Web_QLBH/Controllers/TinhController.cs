using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class TinhController : Controller
	{
		private readonly AppDbContext _context;

		public TinhController(AppDbContext context)
		{
			_context = context;
		}

		// Trang chính hiển thị thông tin cá nhân
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

		// READ - Danh sách Tỉnh 
		public async Task<IActionResult> Tinh_Admin()
		{
			var dsTinh = await _context.Tinh.FromSqlRaw("EXEC sp_Tinh_GetAll")
				.ToListAsync();

			return View(dsTinh);
		}

		// DETAILS - Xem chi tiết
		public async Task<IActionResult> Details(string id)
		{
			var tinh = (await _context.Tinh.FromSqlInterpolated($"EXEC sp_Tinh_GetByID @MaTinh = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (tinh == null)
				return NotFound();

			return View(tinh);
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
		public async Task<IActionResult> Create(Tinh model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
					EXEC sp_Tinh_Insert 
						@TenTinh = {model.TenTinh}
				");

					TempData["SuccessMessage"] = "Thêm tỉnh thành công!";
					return RedirectToAction(nameof(Tinh_Admin));
				}

				catch (Exception ex)
				{
					ModelState.AddModelError("", $"{ex.Message}");

					TempData["ErrorMessage"] = "" + ex.Message;
				}
			}
			return View(model);
		}

		// EDIT - GET
		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var tinh = (await _context.Tinh
				.FromSqlInterpolated($"EXEC sp_Tinh_GetByID @MaTinh = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (tinh == null)
				return NotFound();

			return View(tinh);
		}

		// EDIT - POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Tinh model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
					EXEC sp_Tinh_Update 
						@MaTinh = {model.MaTinh},
						@TenTinh = {model.TenTinh}
				");

					TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
					return RedirectToAction(nameof(Tinh_Admin));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", $"{ex.Message}");

					TempData["ErrorMessage"] = "Lỗi: " + ex.Message;

				}
			}
			return View(model);
		}

		// DELETE - GET
		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var tinh = (await _context.Tinh.FromSqlInterpolated($"EXEC sp_Tinh_GetByID @MaTinh = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (tinh == null)
				return NotFound();

			return View(tinh);
		}

		// DELETE - POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				TempData["ErrorMessage"] = "ID không hợp lệ!";
				return BadRequest();
			}

			var tinh = (await _context.Tinh.FromSqlInterpolated($"EXEC sp_Tinh_GetByID @MaTinh = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (tinh != null)
			{
				await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC sp_Tinh_Delete @MaTinh = {id}");
				TempData["SuccessMessage"] = "Đã xóa tỉnh thành công!";
			}
			else
			{

				TempData["ErrorMessage"] = "Không tìm thấy tỉnh cần xóa!";
			}

			return RedirectToAction(nameof(Tinh_Admin));
		}
	}
}
