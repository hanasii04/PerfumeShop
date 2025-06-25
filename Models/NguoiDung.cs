using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public class NguoiDung : IUpdatable
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string HoTen { get; set; }
		[Phone]
		[StringLength(20)]
		public string SoDienThoai { get; set; }
		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }
		public DateTime NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; }
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
		public virtual List<DiaChi> DiaChis { get; set; } = new List<DiaChi>();
	}
}
