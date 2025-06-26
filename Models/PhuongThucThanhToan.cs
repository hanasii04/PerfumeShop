using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public class PhuongThucThanhToan
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Ten { get; set; }
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
	}
}
