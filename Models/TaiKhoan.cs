using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public enum AccountRole { QuanLy, NhanVien, KhachHang}
	public enum AccountStatus { HoatDong, KhongHoatDong}
	public class TaiKhoan : IUpdatable
	{
		[Key]
		public int Id_TaiKhoan { get; set; }
		[Required]
		[StringLength(100)]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public AccountRole VaiTro { get; set; }
		public AccountStatus TrangThai { get; set; }
		public DateTime NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
		[ForeignKey("Id_NguoiDung")]
		public int Id_NguoiDung { get; set; }
		public virtual NguoiDung NguoiDung { get; set; }
	}
}
