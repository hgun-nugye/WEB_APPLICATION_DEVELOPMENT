using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models
{
	public class Tinh
	{
		[Key]
		public short MaTinh { get; set; }

		[Required, StringLength(90)]
		public string TenTinh { get; set; } = string.Empty;

		[InverseProperty("Tinh")]
		public ICollection<Xa> DsXa { get; set; } = new List<Xa>();
	}
}
