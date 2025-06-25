using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public class PhuongThucVanChuyen
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Ten { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public decimal GiaTien { get; set; }
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
	}
}
