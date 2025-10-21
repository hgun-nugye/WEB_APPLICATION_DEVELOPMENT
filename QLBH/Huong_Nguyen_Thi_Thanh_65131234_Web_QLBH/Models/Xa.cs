using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class Xa
	{

		[Key]
		public required byte MaXa { get; set; }

		[Required]
		[StringLength(90)]
		public required string TenXa { get; set; }

		public required byte MaTinh { get; set; }

		[ForeignKey("MaTinh")]
		public required Tinh Tinh { get; set; }
	}
}
