using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class DonBanHangController : Controller
	{
		private readonly AppDbContext _context;

		public DonBanHangController(AppDbContext context)
		{
			_context = context;
		}

		// READ - Danh sách đơn bán hàng
		public async Task<IActionResult> Index()
		{
			var dsDonBan = await _context.DonBanHang
				.Include(d => d.KhachHang)
				.ToListAsync();
			return View(dsDonBan);
		}

		// DETAILS
		public async Task<IActionResult> Details(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var don = await _context.DonBanHang
				.Include(d => d.KhachHang)
				.Include(d => d.CTBHs)
				.ThenInclude(ct => ct.SanPham)
				.FirstOrDefaultAsync(d => d.MaDBH == id);

			if (don == null)
				return NotFound();

			return View(don);
		}

		// CREATE (GET)
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.MaKHList = new SelectList(_context.KhachHang, "MaKH", "TenKH");
			return View();
		}

		// CREATE (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DonBanHang don)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.DonBanHang.Add(don);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Thêm đơn bán hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaKHList = new SelectList(_context.KhachHang, "MaKH", "TenKH", don.MaKH);
			return View(don);
		}

		// EDIT (GET)
		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var don = await _context.DonBanHang.FindAsync(id);
			if (don == null)
				return NotFound();

			ViewBag.MaKHList = new SelectList(_context.KhachHang, "MaKH", "TenKH", don.MaKH);
			return View(don);
		}

		// EDIT (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DonBanHang don)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.DonBanHang.Update(don);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Cập nhật đơn bán hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = ex.Message;
				}
			}

			ViewBag.MaKHList = new SelectList(_context.KhachHang, "MaKH", "TenKH", don.MaKH);
			return View(don);
		}

		// DELETE (GET)
		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var don = await _context.DonBanHang
				.Include(d => d.KhachHang)
				.FirstOrDefaultAsync(d => d.MaDBH == id);

			if (don == null)
				return NotFound();

			return View(don);
		}

		// DELETE (POST)
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var don = await _context.DonBanHang.FindAsync(id);
				if (don != null)
				{
					_context.DonBanHang.Remove(don);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Xóa đơn bán hàng thành công!";
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
