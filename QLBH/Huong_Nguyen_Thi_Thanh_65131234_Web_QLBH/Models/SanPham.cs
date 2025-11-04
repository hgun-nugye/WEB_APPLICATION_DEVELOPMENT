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

		[Required(ErrorMessage = "Ảnh minh họa không được để trống")]
		[StringLength(50)]
		public required string AnhMH { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn loại sản phẩm")]
		[StringLength(10)]
		public required string MaLoai { get; set; }

		// Khóa ngoại đến GianHang
		[Required, StringLength(10)]
		public string MaGH { get; set; } = null!;

		[ForeignKey(nameof(MaGH))]
		public GianHang? GianHang { get; set; }

		// Khóa ngoại → LoaiSP
		[ForeignKey(nameof(MaLoai))]
		public LoaiSP? LoaiSP { get; set; }
	}
}
