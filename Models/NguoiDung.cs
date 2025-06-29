using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public enum UserRole { QuanLy, NhanVien, KhachHang }
	public enum UserStatus { HoatDong, KhongHoatDong }
	public class NguoiDung 
	{
		[Key]
		public int Id { get; set; }

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

		[ForeignKey("TaiKhoan")]
		public int Id_TaiKhoan { get; set; }
		public virtual TaiKhoan? TaiKhoan { get; set; }

		public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
		public virtual List<DiaChi> DiaChis { get; set; } = new List<DiaChi>();
		public virtual GioHang? GioHang { get; set; }
	}
}
