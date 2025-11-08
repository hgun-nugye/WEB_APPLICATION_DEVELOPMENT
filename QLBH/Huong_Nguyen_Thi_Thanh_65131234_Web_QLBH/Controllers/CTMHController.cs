using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class CTMHController : Controller
	{
		private readonly AppDbContext _context;

		public CTMHController(AppDbContext context)
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

		// ============ READ =============
		public async Task<IActionResult> CTMH_Admin()
		{
			var ctmhList = await _context.CTMH
				.FromSqlRaw("EXEC sp_CTMH_GetAll_Detail")
				.ToListAsync();

			return View(ctmhList);
		}

		// ============ DETAILS ============
		public async Task<IActionResult> Details(string maDMH, string maSP)
		{
			if (string.IsNullOrEmpty(maDMH) || string.IsNullOrEmpty(maSP))
				return NotFound();

			var parameters = new[]
			{
				new SqlParameter("@MaDMH", maDMH),
				new SqlParameter("@MaSP", maSP)
			};

			var data = await _context.CTMH
				.FromSqlRaw("EXEC sp_CTMH_GetById_Detail @MaDMH, @MaSP", parameters)
				.ToListAsync();

			var ctmh = data.FirstOrDefault();

			return View(ctmh);
		}

		// ============ EDIT (GET) ============
		public async Task<IActionResult> Edit(string maDMH, string maSP)
		{
			if (string.IsNullOrEmpty(maDMH) || string.IsNullOrEmpty(maSP)) return NotFound();

			var parameters = new[]
			{
				new SqlParameter("@MaDMH", maDMH),
				new SqlParameter("@MaSP", maSP)
			};

			var data = await _context.CTMH
				.FromSqlRaw("EXEC sp_CTMH_GetById_Detail @MaDMH, @MaSP", parameters)
				.ToListAsync();

			var ctmh = data.FirstOrDefault();
			if (ctmh == null) return NotFound();

			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP", ctmh.MaSP);
			return View(ctmh);
		}

		// ============ EDIT (POST) ============
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CTMH model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var parameters = new[]
					{
						new SqlParameter("@MaDMH", model.MaDMH),
						new SqlParameter("@MaSP", model.MaSP),
						new SqlParameter("@SLM", model.SLM),
						new SqlParameter("@DGM", model.DGM)
					};

					await _context.Database.ExecuteSqlRawAsync(
						"EXEC sp_CTMH_Update @MaDMH, @MaSP, @SLM, @DGM", parameters);

					TempData["SuccessMessage"] = "Cập nhật chi tiết thành công!";
					return RedirectToAction("CTMH_Admin", new { maDMH = model.MaDMH });
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP", model.MaSP);
			return View(model);
		}

		// ============ DELETE (GET) ============
		public async Task<IActionResult> Delete(string maDMH, string maSP)
		{
			if (string.IsNullOrEmpty(maDMH) || string.IsNullOrEmpty(maSP))
				return NotFound();

			var parameters = new[]
			{
		new SqlParameter("@MaDMH", maDMH),
		new SqlParameter("@MaSP", maSP)
	};

			// Lấy chi tiết mua hàng kèm tên sản phẩm
			var data = await _context.CTMH
				.FromSqlRaw("EXEC sp_CTMH_GetById_Detail @MaDMH, @MaSP", parameters)
				.ToListAsync();

			var ctmh = data.FirstOrDefault();
			if (ctmh == null) return NotFound();

			return View(ctmh);
		}

		// ============ DELETE (POST) ============
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string maDMH, string maSP)
		{
			try
			{
				var parameters = new[]
				{
			new SqlParameter("@MaDMH", maDMH),
			new SqlParameter("@MaSP", maSP)
		};

				await _context.Database.ExecuteSqlRawAsync(
					"EXEC sp_CTMH_Delete @MaDMH, @MaSP", parameters);

				TempData["SuccessMessage"] = "Xóa chi tiết mua hàng thành công!";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			return RedirectToAction("CTMH_Admin", new { maDMH });
		}

	}
}
