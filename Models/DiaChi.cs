using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class DiaChi
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		public string TenDiaChi { get; set; }
		[ForeignKey("NguoiDung")]
		public int Id_NguoiDung { get; set; }
		public virtual NguoiDung NguoiDung { get; set; }
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
	}
}
