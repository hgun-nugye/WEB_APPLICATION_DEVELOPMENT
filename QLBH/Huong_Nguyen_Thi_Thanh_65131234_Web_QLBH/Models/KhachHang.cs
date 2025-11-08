using System.ComponentModel.DataAnnotations;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class KhachHang
	{
		[Key]
		[StringLength(10)]
		public required string MaKH { get; set; }

		[Required]
		[StringLength(50)]
		public required string TenKH { get; set; }

		[StringLength(15)]
		public required string DienThoaiKH { get; set; }

		[StringLength(255)]
		public required string EmailKH { get; set; }

		[StringLength(255)]
		public required string DiaChiKH { get; set; }

		public virtual ICollection<DonBanHang>? DonBanHangs { get; set; }
	}
}
