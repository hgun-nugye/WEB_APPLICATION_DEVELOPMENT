using System.ComponentModel.DataAnnotations;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class Tinh
	{

		[Key]
		public required byte MaTinh { get; set; }

		[Required]
		[StringLength(90)]
		public required string TenTinh { get; set; }

		public ICollection<Xa> MaXa { get; set; } = new List<Xa>();
	}
}
