using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class XaController : Controller
	{
		private readonly AppDbContext _context;

		public XaController(AppDbContext context)
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

		//READ - Danh sách Xã
		public async Task<IActionResult> Xa_Admin()
		{
			var dsXa = await _context.Xa.Join(
					_context.Tinh,
					x => x.MaTinh,
					t => t.MaTinh,
					(x, t) => new Xa
					{
						MaXa = x.MaXa,
						TenXa = x.TenXa,
						MaTinh = x.MaTinh,
						TenTinh = t.TenTinh
					}
				)
				.ToListAsync();

			return View(dsXa);
		}

		// DETAILS - Xem chi tiết
		public async Task<IActionResult> Details(string id)
		{
			var xa = (await _context.Xa.FromSqlInterpolated($"EXEC sp_Xa_GetByID @MaXa = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (xa == null)
				return NotFound();

			return View(xa);
		}

		// CREATE - GET
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.MaTinhList = new SelectList(_context.Tinh, "MaTinh", "TenTinh");
			return View();
		}

		// CREATE - POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Xa model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
				EXEC sp_Xa_Insert 
					@TenXa = {model.TenXa},
					@MaTinh = {model.MaTinh}
			");

					TempData["SuccessMessage"] = "Thêm xã thành công!";
					return RedirectToAction(nameof(Xa_Admin));
				}
				
				catch (Exception ex)
				{
					ModelState.AddModelError("", $"{ex.Message}");

					TempData["ErrorMessage"] = "Lỗi: " + ex.Message;

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

			var xa = (await _context.Xa
				.FromSqlInterpolated($"EXEC sp_Xa_GetByID @MaXa = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (xa == null)
				return NotFound();

			return View(xa);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Xa model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
				EXEC sp_Xa_Update 
					@MaXa = {model.MaXa},
					@TenXa = {model.TenXa},
					@MaTinh = {model.MaTinh}
			");

					TempData["SuccessMessage"] = "Cập nhật xã thành công!";
					return RedirectToAction(nameof(Xa_Admin));
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

			var xa = (await _context.Xa.FromSqlInterpolated($"EXEC sp_Xa_GetByID @MaXa = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (xa == null)
				return NotFound();

			return View(xa);
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

			var xa = (await _context.Xa.FromSqlInterpolated($"EXEC sp_Xa_GetByID @MaXa = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (xa != null)
			{
				await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC sp_Xa_Delete @MaXa = {id}");
				TempData["SuccessMessage"] = "Đã xóa xã thành công!";
			}
			else
			{
				TempData["ErrorMessage"] = "Không tìm thấy xã cần xóa!";
			}

			return RedirectToAction(nameof(Xa_Admin));
		}
	}
}
