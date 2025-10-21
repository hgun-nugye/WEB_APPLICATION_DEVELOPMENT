using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;
using Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Controllers
{
	public class NhaCCController : Controller
	{
		private readonly AppDbContext _context;

		public NhaCCController(AppDbContext context)
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

		// READ - Danh sách Nhà cung cấp
		public async Task<IActionResult> NhaCC_Admin()
		{
			var dsNhaCC = await _context.NhaCC.FromSqlRaw("EXEC sp_NhaCC_GetAll")
				.ToListAsync();

			return View(dsNhaCC);
		}

		// DETAILS - Xem chi tiết
		public async Task<IActionResult> Details(string id)
		{
			var ncc = (await _context.NhaCC.FromSqlInterpolated($"EXEC sp_NhaCC_GetByID @MaNCC = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (ncc == null)
				return NotFound();

			return View(ncc);
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
		public async Task<IActionResult> Create(NhaCC model)
		{
			try
			{
				await _context.Database.ExecuteSqlInterpolatedAsync($@"
					EXEC sp_NhaCC_Insert 
						@TenNCC = {model.TenNCC}, 
						@DienThoaiNCC = {model.DienThoaiNCC}, 
						@EmailNCC = {model.EmailNCC}, 
						@DiaChiNCC = {model.DiaChiNCC}
				");


				TempData["SuccessMessage"] = "Thêm nhà cung cấp thành công!";
				return RedirectToAction(nameof(NhaCC_Admin));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
				return View(model);
			}
		}

		// EDIT - GET
		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var ncc = (await _context.NhaCC
				.FromSqlInterpolated($"EXEC sp_NhaCC_GetByID @MaNCC = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (ncc == null)
				return NotFound();

			return View(ncc);
		}

		// EDIT - POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(NhaCC model)
		{
			if (ModelState.IsValid)
			{
				// Nếu Update không trả về dữ liệu, ta dùng ExecuteSqlInterpolatedAsync
				await _context.Database.ExecuteSqlInterpolatedAsync($@"
					EXEC sp_NhaCC_Update 
						@MaNCC = {model.MaNCC},
						@TenNCC = {model.TenNCC}, 
						@DienThoaiNCC = {model.DienThoaiNCC}, 
						@EmailNCC = {model.EmailNCC}, 
						@DiaChiNCC = {model.DiaChiNCC}
				");

				TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
				return RedirectToAction(nameof(NhaCC_Admin));
			}
			return View(model);
		}

		// DELETE - GET
		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();

			var ncc = (await _context.NhaCC.FromSqlInterpolated($"EXEC sp_NhaCC_GetByID @MaNCC = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (ncc == null)
				return NotFound();

			return View(ncc);
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

			// Kiểm tra tồn tại
			var ncc = (await _context.NhaCC.FromSqlInterpolated($"EXEC sp_NhaCC_GetByID @MaNCC = {id}")
				.ToListAsync())
				.FirstOrDefault();

			if (ncc != null)
			{
				await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC sp_NhaCC_Delete @MaNCC = {id}");
				TempData["SuccessMessage"] = "Đã xóa nhà cung cấp thành công!";
			}
			else
			{
				TempData["ErrorMessage"] = "Không tìm thấy nhà cung cấp cần xóa!";
			}

			return RedirectToAction(nameof(NhaCC_Admin));
		}
	}
}
