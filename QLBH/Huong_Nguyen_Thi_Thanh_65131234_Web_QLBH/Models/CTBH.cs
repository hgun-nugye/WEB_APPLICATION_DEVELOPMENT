using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	[Table("CTBH")]
	public class CTBH
	{
		[Key, Column(Order = 0)]
		[StringLength(11)]
		[Display(Name = "Mã đơn bán hàng")]
		public required string MaDBH { get; set; }

		[Key, Column(Order = 1)]
		[StringLength(10)]
		[Display(Name = "Mã sản phẩm")]
		public required string MaSP { get; set; }

		[Required(ErrorMessage = "Số lượng bán không được để trống")]
		[Display(Name = "Số lượng bán")]
		[Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
		public int SLB { get; set; }

		[Required(ErrorMessage = "Đơn giá bán không được để trống")]
		[Display(Name = "Đơn giá bán")]
		[Column(TypeName = "money")]
		public decimal DGB { get; set; }

		// 🔗 Khóa ngoại đến DonBanHang
		[ForeignKey("MaDBH")]
		public virtual DonBanHang? DonBanHang { get; set; }

		// 🔗 Khóa ngoại đến SanPham
		[ForeignKey("MaSP")]
		public virtual SanPham? SanPham { get; set; }
	}
}
