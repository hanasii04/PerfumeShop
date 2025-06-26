using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public enum UserRole { QuanLy, NhanVien, KhachHang }
	public enum UserStatus { HoatDong, KhongHoatDong }
	public class NguoiDung : IUpdatable
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public UserRole VaiTro { get; set; }
		public UserStatus TrangThai { get; set; }
		[Required]
		[StringLength(100)]
		public string HoTen { get; set; }
		[Phone]
		[StringLength(10)]
		public string SoDienThoai { get; set; }
		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; }
		public DateTime NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; }
		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
		public virtual List<DiaChi> DiaChis { get; set; } = new List<DiaChi>();
		public virtual GioHang GioHang { get; set; }
	}
}
