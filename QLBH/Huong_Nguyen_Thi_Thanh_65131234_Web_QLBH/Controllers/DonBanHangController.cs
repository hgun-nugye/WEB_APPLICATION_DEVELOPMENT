using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class DonBanHangController : Controller
	{
		private readonly AppDbContext _context;

		public DonBanHangController(AppDbContext context)
		{
			_context = context;
		}

		// ============ INDEX ============
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

		// ============ READ ============
		public async Task<IActionResult> DonBanHang_Admin()
		{
			var data = await _context.DonBanHang
				.FromSqlRaw("EXEC sp_DonBanHang_GetAll_Detail")
				.ToListAsync();

			return View(data);
		}

		// ============ DETAILS ============
		public async Task<IActionResult> Details(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDBH", id);
			var result = await _context.DonBanHang
				.FromSqlRaw("EXEC sp_DonBanHang_GetById_Detail @MaDBH", param)
				.ToListAsync();

			var dbh = result.FirstOrDefault();
			if (dbh == null) return NotFound();

			return View(dbh);
		}

		// ===================== CREATE (GET) =====================
		public IActionResult Create()
		{
			ViewBag.MaKH = new SelectList(_context.KhachHang, "MaKH", "TenKH");
			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP");

			var model = new DonBanHang
			{
				NgayBH = DateTime.Today,
				CTBHs = new List<CTBH>
				{
					new CTBH()
				}
			};

			return View(model);
		}

		// ===================== CREATE (POST) =====================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DonBanHang model)
		{
			try
			{
				if (ModelState.IsValid && model.CTBHs != null && model.CTBHs.Any())
				{
					var table = new DataTable();
					table.Columns.Add("MaSP", typeof(string));
					table.Columns.Add("SLB", typeof(int));
					table.Columns.Add("DGB", typeof(decimal));

					foreach (var ct in model.CTBHs)
					{
						table.Rows.Add(ct.MaSP, ct.SLB, ct.DGB);
					}

					var parameters = new[]
					{
						new SqlParameter("@NgayBH", model.NgayBH),
						new SqlParameter("@MaKH", model.MaKH),
						new SqlParameter("@ChiTiet", table)
						{
							SqlDbType = SqlDbType.Structured,
							TypeName = "dbo.CTBH_List"
						}
					};
					await _context.Database.ExecuteSqlRawAsync("EXEC sp_DonBanHang_Insert @NgayBH, @MaKH, @ChiTiet", parameters);

					TempData["SuccessMessage"] = $"Thêm đơn bán hàng thành công!";
					return RedirectToAction(nameof(DonBanHang_Admin));
				}

				TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin đơn hàng và chi tiết sản phẩm.";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Lỗi khi thêm đơn bán hàng: " + ex.Message;
			}

			ViewBag.MaKH = new SelectList(_context.KhachHang, "MaKH", "TenKH", model.MaKH);
			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP");
			return View(model);
		}

		// ============ EDIT (GET) ============
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDBH", id);
			var data = await _context.DonBanHang
				.FromSqlRaw("EXEC sp_DonBanHang_GetById_Detail @MaDBH", param)
				.ToListAsync();

			var dbh = data.FirstOrDefault();
			if (dbh == null) return NotFound();

			ViewBag.MaKH = new SelectList(_context.KhachHang, "MaKH", "TenKH", dbh.MaKH);
			return View(dbh);
		}

		// ============ EDIT (POST) ============
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DonBanHang model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var parameters = new[]
					{
						new SqlParameter("@MaDBH", model.MaDBH),
						new SqlParameter("@NgayBH", model.NgayBH),
						new SqlParameter("@MaKH", model.MaKH)
					};

					await _context.Database.ExecuteSqlRawAsync(
						"EXEC sp_DonBanHang_Update @MaDBH, @NgayBH, @MaKH", parameters);

					TempData["SuccessMessage"] = "Cập nhật đơn bán hàng thành công!";
					return RedirectToAction(nameof(DonBanHang_Admin));
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			ViewBag.MaKH = new SelectList(_context.KhachHang, "MaKH", "TenKH", model.MaKH);
			return View(model);
		}

		// ============ DELETE (GET) ============
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDBH", id);
			var data = await _context.DonBanHang
				.FromSqlRaw("EXEC sp_DonBanHang_GetById_Detail @MaDBH", param)
				.ToListAsync();

			var dbh = data.FirstOrDefault();
			if (dbh == null) return NotFound();

			return View(dbh);
		}

		// ============ DELETE (POST) ============
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var param = new SqlParameter("@MaDBH", id);
				await _context.Database.ExecuteSqlRawAsync("EXEC sp_DonBanHang_Delete @MaDBH", param);

				TempData["SuccessMessage"] = "Xóa đơn bán hàng thành công!";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			return RedirectToAction(nameof(DonBanHang_Admin));
		}
	}
}
