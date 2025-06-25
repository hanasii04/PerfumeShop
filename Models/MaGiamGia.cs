using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public class MaGiamGia : IUpdatable
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string TenMaGiamGia { get; set; }
		[Required]
		[Range(0, 100)]
		public decimal PhanTramGiam { get; set; }
		[Required]
		public DateTime NgayBatDau { get; set; }
		[Required]
		public DateTime NgayKetThuc { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int SoLuong { get; set; }
		public DateTime NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
	}
}
