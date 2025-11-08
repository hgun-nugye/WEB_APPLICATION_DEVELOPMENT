using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class CTBHController : Controller
	{
		private readonly AppDbContext _context;

		public CTBHController(AppDbContext context)
		{
			_context = context;
		}

		// READ - Danh sách chi tiết bán hàng
		public async Task<IActionResult> Index()
		{
			var dsCTBH = await _context.CTBH
				.Include(c => c.DonBanHang)
				.Include(c => c.SanPham)
				.ToListAsync();
			return View(dsCTBH);
		}

		// DETAILS
		public async Task<IActionResult> Details(string MaDBH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDBH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTBH
				.Include(c => c.DonBanHang)
				.Include(c => c.SanPham)
				.FirstOrDefaultAsync(c => c.MaDBH == MaDBH && c.MaSP == MaSP);

			if (ct == null)
				return NotFound();

			return View(ct);
		}

		// CREATE (GET)
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.MaDBHList = new SelectList(_context.DonBanHang, "MaDBH", "MaDBH");
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP");
			return View();
		}

		// CREATE (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CTBH ct)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.CTBH.Add(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Thêm chi tiết bán hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaDBHList = new SelectList(_context.DonBanHang, "MaDBH", "MaDBH", ct.MaDBH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);
			return View(ct);
		}

		// EDIT (GET)
		[HttpGet]
		public async Task<IActionResult> Edit(string MaDBH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDBH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTBH.FindAsync(MaDBH, MaSP);
			if (ct == null)
				return NotFound();

			ViewBag.MaDBHList = new SelectList(_context.DonBanHang, "MaDBH", "MaDBH", ct.MaDBH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);
			return View(ct);
		}

		// EDIT (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CTBH ct)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.CTBH.Update(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Cập nhật chi tiết bán hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaDBHList = new SelectList(_context.DonBanHang, "MaDBH", "MaDBH", ct.MaDBH);
			ViewBag.MaSPList = new SelectList(_context.SanPham, "MaSP", "TenSP", ct.MaSP);
			return View(ct);
		}

		// DELETE (GET)
		[HttpGet]
		public async Task<IActionResult> Delete(string MaDBH, string MaSP)
		{
			if (string.IsNullOrEmpty(MaDBH) || string.IsNullOrEmpty(MaSP))
				return BadRequest();

			var ct = await _context.CTBH
				.Include(c => c.DonBanHang)
				.Include(c => c.SanPham)
				.FirstOrDefaultAsync(c => c.MaDBH == MaDBH && c.MaSP == MaSP);

			if (ct == null)
				return NotFound();

			return View(ct);
		}

		// DELETE (POST)
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string MaDBH, string MaSP)
		{
			try
			{
				var ct = await _context.CTBH.FindAsync(MaDBH, MaSP);
				if (ct != null)
				{
					_context.CTBH.Remove(ct);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Xóa chi tiết bán hàng thành công!";
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
