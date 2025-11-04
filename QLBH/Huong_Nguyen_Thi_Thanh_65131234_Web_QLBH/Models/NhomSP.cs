using System.ComponentModel.DataAnnotations;
namespace Huong_Nguyen_Thi_Thanh_65131234_Web_QLBH.Models;

public class NhomSP
{
	[Key]
	public string? MaNhom { get; set; }

	[Required(ErrorMessage = "Tên nhóm không được để trống!")]
	[StringLength(100)]
	public string? TenNhom { get; set; }

	public ICollection<LoaiSP>? LoaiSPs { get; set; }

}
