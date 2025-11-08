using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class DonMuaHangController : Controller
	{

		private readonly AppDbContext _context;

		public DonMuaHangController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		// ===== INDEX =====
		public async Task<IActionResult> DonMuaHang_Admin()
		{
			var data = await _context.DonMuaHang
				.Include(d => d.NhaCC)
				.ToListAsync();
			return View(data);
		}

		// ===== DETAILS =====
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
				return NotFound();

			var dmh = await _context.DonMuaHang
				.Include(d => d.NhaCC)
				.FirstOrDefaultAsync(m => m.MaDMH == id);

			if (dmh == null)
				return NotFound();

			return View(dmh);
		}

		// ===== CREATE (GET) =====
		public IActionResult Create()
		{
			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC");
			return View();
		}

		// ===== CREATE (POST) =====
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DonMuaHang dmh)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Add(dmh);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Thêm đơn mua hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", dmh.MaNCC);
			return View(dmh);
		}

		// ===== EDIT (GET) =====
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
				return NotFound();

			var dmh = await _context.DonMuaHang.FindAsync(id);
			if (dmh == null)
				return NotFound();

			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", dmh.MaNCC);
			return View(dmh);
		}

		// ===== EDIT (POST) =====
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DonMuaHang dmh)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Update(dmh);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Cập nhật đơn mua hàng thành công!";
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", dmh.MaNCC);
			return View(dmh);
		}

		// ===== DELETE (GET) =====
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
				return NotFound();

			var dmh = await _context.DonMuaHang
				.Include(d => d.NhaCC)
				.FirstOrDefaultAsync(m => m.MaDMH == id);

			if (dmh == null)
				return NotFound();

			return View(dmh);
		}

		// ===== DELETE (POST) =====
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var dmh = await _context.DonMuaHang.FindAsync(id);
				if (dmh != null)
				{
					_context.DonMuaHang.Remove(dmh);
					await _context.SaveChangesAsync();
					TempData["SuccessMessage"] = "Xóa đơn mua hàng thành công!";
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
