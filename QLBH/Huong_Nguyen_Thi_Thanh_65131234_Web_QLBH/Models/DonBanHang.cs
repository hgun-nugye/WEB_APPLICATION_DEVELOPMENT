using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class DonBanHang
	{
		[Key]
		[StringLength(11)]
		[Display(Name = "Mã đơn bán hàng")]
		public required string MaDBH { get; set; }

		[Required(ErrorMessage = "Ngày bán hàng không được để trống")]
		[Display(Name = "Ngày bán hàng")]
		[DataType(DataType.Date)]
		public DateTime NgayBH { get; set; }

		[Required(ErrorMessage = "Khách hàng không được để trống")]
		[StringLength(10)]
		[Display(Name = "Mã khách hàng")]
		public required string MaKH { get; set; }

		[ForeignKey("MaKH")]
		public virtual KhachHang? KhachHang { get; set; }

		public virtual ICollection<CTBH>? CTBHs { get; set; }
	}
}
