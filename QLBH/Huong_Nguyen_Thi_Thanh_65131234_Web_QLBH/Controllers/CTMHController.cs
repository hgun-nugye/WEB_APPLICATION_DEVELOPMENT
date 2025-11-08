using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		// READ - Danh sách chi tiết mua hàng
		public async Task<IActionResult> Index()
		{
			var dsCTMH = await _context.CTMH
				.Include(c => c.DonMuaHang)
				.Include(c => c.SanPham)
				.ToListAsync();
			return View(dsCTMH);
		}

		// DETAILS
		public async Task<IActionResult> Details(string MaDMH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDMH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTMH
				.Include(c => c.DonMuaHang)
				.Include(c => c.SanPham)
				.FirstOrDefaultAsync(c => c.MaDMH == MaDMH && c.MaSP == MaSP);

			if (ct == null)
				return NotFound();

			return View(ct);
		}

		// CREATE (GET)
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.MaDMHList = new SelectList(_context.DonMuaHang, "MaDMH", "MaDMH");
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP");
			return View();
		}

		// CREATE (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CTMH ct)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.CTMH.Add(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Thêm chi tiết mua hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaDMHList = new SelectList(_context.DonMuaHang, "MaDMH", "MaDMH", ct.MaDMH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);
			return View(ct);
		}

		// EDIT (GET)
		[HttpGet]
		public async Task<IActionResult> Edit(string MaDMH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDMH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTMH.FindAsync(MaDMH, MaSP);
			if (ct == null)
				return NotFound();

			ViewBag.MaDMHList = new SelectList(_context.DonMuaHang, "MaDMH", "MaDMH", ct.MaDMH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);

			return View(ct);
		}

		// EDIT (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CTMH ct)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.CTMH.Update(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Cập nhật chi tiết mua hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaDMHList = new SelectList(_context.DonMuaHang, "MaDMH", "MaDMH", ct.MaDMH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);
			return View(ct);
		}

		// DELETE (GET)
		[HttpGet]
		public async Task<IActionResult> Delete(string MaDMH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDMH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTMH
				.Include(c => c.DonMuaHang)
				.Include(c => c.SanPham)
				.FirstOrDefaultAsync(c => c.MaDMH == MaDMH && c.MaSP == MaSP);

			if (ct == null)
				return NotFound();

			return View(ct);
		}

		// DELETE (POST)
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string MaDMH, string MaSP)
		{
			try
			{
				var ct = await _context.CTMH.FindAsync(MaDMH, MaSP);
				if (ct != null)
				{
					_context.CTMH.Remove(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Xóa chi tiết mua hàng thành công!";
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
