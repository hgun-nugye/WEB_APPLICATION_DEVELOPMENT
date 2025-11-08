using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class DonMuaHangController : Controller
	{
		private readonly AppDbContext _context;

		public DonMuaHangController(AppDbContext context)
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
		public async Task<IActionResult> DonMuaHang_Admin()
		{
			var data = await _context.DonMuaHang
				.FromSqlRaw("EXEC sp_DonMuaHang_GetAll_Detail")
				.ToListAsync();

			return View(data);
		}

		// ============ DETAILS ============
		public async Task<IActionResult> Details(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDMH", id);

			var result = await _context.DonMuaHang
				.FromSqlRaw("EXEC sp_DonMuaHang_GetById_Detail @MaDMH", param)
				.ToListAsync();

			var dmh = result.FirstOrDefault();
			if (dmh == null) return NotFound();

			return View(dmh);
		}

		// ===================== CREATE (GET) =====================
		public IActionResult Create()
		{
			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC");
			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP");

			// Khởi tạo mẫu đơn rỗng với 1 dòng chi tiết để nhập
			var model = new DonMuaHang
			{
				NgayMH = DateTime.Today,
				CTMHs = new List<CTMH>
				{
					new CTMH()
				}
			};

			return View(model);
		}

		// ===================== CREATE (POST) =====================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DonMuaHang model)
		{
			try
			{
				if (ModelState.IsValid && model.CTMHs != null && model.CTMHs.Any())
				{
					// Tạo DataTable để truyền vào proc
					var table = new DataTable();
					table.Columns.Add("MaSP", typeof(string));
					table.Columns.Add("SLM", typeof(int));
					table.Columns.Add("DGM", typeof(decimal));

					foreach (var ct in model.CTMHs)
					{
						table.Rows.Add(ct.MaSP, ct.SLM, ct.DGM);
					}

					// Tạo danh sách parameter cho procedure
					var parameters = new[]
					{
						new SqlParameter("@NgayMH", model.NgayMH),
						new SqlParameter("@MaNCC", model.MaNCC),
						new SqlParameter("@ChiTiet", table)
						{
							SqlDbType = SqlDbType.Structured,
							TypeName = "dbo.CTMH_List"
						}
					};

					await _context.Database.ExecuteSqlRawAsync(
					"EXEC sp_DonMuaHang_Insert @NgayMH, @MaNCC, @ChiTiet", parameters);

					TempData["SuccessMessage"] = "Thêm đơn mua hàng và chi tiết thành công!";
					return RedirectToAction("DonMuaHang_Admin");

				}

				TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin đơn hàng và chi tiết sản phẩm.";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Lỗi khi thêm đơn hàng: " + ex.Message;
			}

			// Nếu lỗi, nạp lại dropdown
			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", model.MaNCC);
			ViewBag.MaSP = new SelectList(_context.SanPham, "MaSP", "TenSP");
			return View(model);
		}

		// ============ EDIT (GET) ============
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDMH", id);
			var data = await _context.DonMuaHang
				.FromSqlRaw("EXEC sp_DonMuaHang_GetById_Detail @MaDMH", param)
				.ToListAsync();

			var dmh = data.FirstOrDefault();
			if (dmh == null) return NotFound();

			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", dmh.MaNCC);
			return View(dmh);
		}

		// ============ EDIT (POST) ============
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(DonMuaHang model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var parameters = new[]
					{
						new SqlParameter("@MaDMH", model.MaDMH),
						new SqlParameter("@NgayMH", model.NgayMH),
						new SqlParameter("@MaNCC", model.MaNCC)
					};

					await _context.Database.ExecuteSqlRawAsync("EXEC sp_DonMuaHang_Update @MaDMH, @NgayMH, @MaNCC", parameters);

					TempData["SuccessMessage"] = "Cập nhật đơn mua hàng thành công!";
					return RedirectToAction(nameof(DonMuaHang_Admin));
				}
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			ViewBag.MaNCC = new SelectList(_context.NhaCC, "MaNCC", "TenNCC", model.MaNCC);
			return View(model);
		}

		// ============ DELETE (GET) ============
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id)) return NotFound();

			var param = new SqlParameter("@MaDMH", id);
			var data = await _context.DonMuaHang
				.FromSqlRaw("EXEC sp_DonMuaHang_GetById_Detail @MaDMH", param)
				.ToListAsync();

			var dmh = data.FirstOrDefault();
			if (dmh == null) return NotFound();

			return View(dmh);
		}

		// ============ DELETE (POST) ============
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			try
			{
				var param = new SqlParameter("@MaDMH", id);
				await _context.Database.ExecuteSqlRawAsync("EXEC sp_DonMuaHang_Delete @MaDMH", param);

				TempData["SuccessMessage"] = "Xóa đơn mua hàng thành công!";
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			return RedirectToAction(nameof(DonMuaHang_Admin));
		}
	}
}
