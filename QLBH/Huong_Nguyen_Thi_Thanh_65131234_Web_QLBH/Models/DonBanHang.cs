using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class DonBanHang
	{
		[Key]
		[StringLength(11)]
		[Display(Name = "Mã đơn bán hàng")]
		public string? MaDBH { get; set; }

		[Required(ErrorMessage = "Ngày bán hàng không được để trống")]
		[Display(Name = "Ngày bán hàng")]
		[DataType(DataType.Date)]
		public DateTime NgayBH { get; set; }

		[StringLength(10)]
		[Display(Name = "Mã khách hàng")]
		public string? MaKH { get; set; }

		[ForeignKey("MaKH")]
		public virtual KhachHang? KhachHang { get; set; }

		public virtual List<CTBH>? CTBHs { get; set; }

		public string? TenKH { get; set; }
	}
	[Keyless] // vì không có khóa chính riêng
	public class DonBanHangDetail
	{
		public string? MaDBH { get; set; }
		public DateTime NgayBH { get; set; }
		public string? MaKH { get; set; }
		public string? TenKH { get; set; }
		public string? TenSP { get; set; }
		public int? SLB { get; set; }
		public decimal? DGB { get; set; }
	}

}
