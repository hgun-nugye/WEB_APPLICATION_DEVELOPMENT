using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class SanPham
	{
		[Key]
		[StringLength(10)]
		public string? MaSP { get; set; }

		[Required(ErrorMessage = "Tên sản phẩm không được để trống")]
		[StringLength(50)]
		public required string TenSP { get; set; }

		[Required(ErrorMessage = "Đơn giá không được để trống")]
		[Column(TypeName = "money")]
		public decimal DonGia { get; set; }

		[Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
		[Column(TypeName = "text")]
		public required string MoTaSP { get; set; }

		[StringLength(50)]
		public string? AnhMH { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
		[StringLength(10)]
		public required string MaLoai { get; set; }

		[Required(ErrorMessage = "Mã gian hàng không được để trống")]
		[StringLength(10)]
		public required string MaGH { get; set; } = null!;

		// Khóa ngoại
		[ForeignKey(nameof(MaGH))]
		public GianHang? GianHang { get; set; }

		[ForeignKey(nameof(MaLoai))]
		public LoaiSP? LoaiSP { get; set; }

		// Các tập hợp ngược
		public virtual ICollection<CTMH>? CTMHs { get; set; }
		public virtual ICollection<CTBH>? CTBHs { get; set; }

		// ====== Các thuộc tính không ánh xạ (chỉ dùng hiển thị) ======
		[NotMapped]
		public string? TenGH { get; set; }

		[NotMapped]
		public string? TenLoai { get; set; }

		[NotMapped] // không ánh xạ DB, chỉ dùng upload
		public IFormFile? AnhFile { get; set; }

	}
}
