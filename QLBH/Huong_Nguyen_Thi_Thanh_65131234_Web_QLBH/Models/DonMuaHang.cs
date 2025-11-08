using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class DonMuaHang
	{
		[Key]
		[StringLength(11)]
		[Display(Name = "Mã đơn mua hàng")]
		public string? MaDMH { get; set; }

		[Required(ErrorMessage = "Ngày mua hàng không được để trống")]
		[Display(Name = "Ngày mua hàng")]
		[DataType(DataType.Date)]
		public DateTime NgayMH { get; set; }

		[StringLength(10)]
		[Display(Name = "Mã nhà cung cấp")]
		public string? MaNCC { get; set; }

		[ForeignKey("MaNCC")]
		public virtual NhaCC? NhaCC { get; set; }

		public virtual List<CTMH>? CTMHs { get; set; }

		public string? TenNCC { get; set; }
	}
}
