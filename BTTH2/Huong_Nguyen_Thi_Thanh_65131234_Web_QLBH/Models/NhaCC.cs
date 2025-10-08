using System.ComponentModel.DataAnnotations;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class NhaCC
	{
		[Key]
		[StringLength(10)]
		public required string MaNCC { get; set; }

		[Required]
		[StringLength(100)]
		public required string TenNCC { get; set; }

		[StringLength(15)]
		public required string DienThoaiNCC { get; set; }

		[StringLength(100)]
		public required string EmailNCC { get; set; }

		[StringLength(200)]
		public required string DiaChiNCC { get; set; }
	}
}
