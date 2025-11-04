using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class SanPhamController : Controller
	{
		private readonly AppDbContext _context;

		public SanPhamController(AppDbContext context)
		{
			_context = context;
		}

		// READ - Danh sách sản phẩm
		public async Task<IActionResult> SanPham_Admin()
		{
			var dsSanPham = await _context.SanPham
				.FromSqlRaw("EXEC sp_SanPham_GetAll")
				.Include(sp => sp.LoaiSP)
				.ToListAsync();

			return View(dsSanPham);
		}

		// DETAILS - Xem chi tiết
		public async Task<IActionResult> Details(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var sp = (await _context.SanPham
				.FromSqlInterpolated($"EXEC sp_SanPham_GetByID @MaSP = {id}")
				.Include(x => x.LoaiSP)
				.ToListAsync())
				.FirstOrDefault();

			if (sp == null)
				return NotFound();

			return View(sp);
		}

		// CREATE (GET)
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.MaLoaiList = new SelectList(_context.LoaiSP, "MaLoai", "TenLSP");
			return View();
		}

		// CREATE (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SanPham model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        EXEC sp_SanPham_Insert 
                            @TenSP = N'{model.TenSP}',
                            @DonGia = {model.DonGia},
                            @MoTaSP = N'{model.MoTaSP}',
                            @AnhMH = N'{model.AnhMH}',
                            @MaLoai = {model.MaLoai},
							@MaGH = {model.MaGH}
                    ");

					TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
					return RedirectToAction(nameof(SanPham_Admin));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
					TempData["ErrorMessage"] = ex.Message;
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
			}

			ViewBag.MaLoaiList = new SelectList(_context.LoaiSP, "MaLoai", "TenLSP", model.MaLoai);
			return View(model);
		}

		// EDIT (GET)
		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var sp = (await _context.SanPham
				.FromSqlInterpolated($"EXEC sp_SanPham_GetByID @MaSP = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (sp == null)
				return NotFound();

			ViewBag.MaLoaiList = new SelectList(_context.LoaiSP, "MaLoai", "TenLSP", sp.MaLoai);
			return View(sp);
		}

		// EDIT (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(SanPham model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _context.Database.ExecuteSqlInterpolatedAsync($@"
                        EXEC sp_SanPham_Update 
                            @MaSP = {model.MaSP},
                            @TenSP = N'{model.TenSP}',
                            @DonGia = {model.DonGia},
                            @MoTaSP = N'{model.MoTaSP}',
                            @AnhMH = N'{model.AnhMH}',
                            @MaLoai = {model.MaLoai},
							@MaGH = {model.MaGH}
                    ");

					TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
					return RedirectToAction(nameof(SanPham_Admin));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaLoaiList = new SelectList(_context.LoaiSP, "MaLoai", "TenLSP", model.MaLoai);
			return View(model);
		}

		// DELETE (GET)
		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var sp = (await _context.SanPham
				.FromSqlInterpolated($"EXEC sp_SanPham_GetByID @MaSP = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (sp == null)
				return NotFound();

			return View(sp);
		}

		// DELETE (POST)
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				TempData["ErrorMessage"] = "Mã sản phẩm không hợp lệ!";
				return RedirectToAction(nameof(SanPham_Admin));
			}

			await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC sp_SanPham_Delete @MaSP = {id}");
			TempData["SuccessMessage"] = "Đã xóa sản phẩm thành công!";

			return RedirectToAction(nameof(SanPham_Admin));
		}
	}
}
