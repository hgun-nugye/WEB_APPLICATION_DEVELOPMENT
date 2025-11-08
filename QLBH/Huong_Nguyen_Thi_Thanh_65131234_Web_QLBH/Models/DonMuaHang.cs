using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class DonMuaHang
	{
		[Key]
		[StringLength(11)]
		[Display(Name = "Mã đơn mua hàng")]
		public required string MaDMH { get; set; }

		[Required(ErrorMessage = "Ngày mua hàng không được để trống")]
		[Display(Name = "Ngày mua hàng")]
		[DataType(DataType.Date)]
		public DateTime NgayMH { get; set; }

		[Required(ErrorMessage = "Nhà cung cấp không được để trống")]
		[StringLength(10)]
		[Display(Name = "Mã nhà cung cấp")]
		public required string MaNCC { get; set; }

		[ForeignKey("MaNCC")]
		public virtual NhaCC? NhaCC { get; set; }

		public virtual ICollection<CTMH>? CTMHs { get; set; }
	}
}
