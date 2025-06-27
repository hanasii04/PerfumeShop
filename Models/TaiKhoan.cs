using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
    public enum Role { QuanLy, NhanVien, KhachHang }
    public enum Status { HoatDong, KhongHoatDong }

    public class TaiKhoan
    {
        [Key]
        public int Id  { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public Role VaiTro { get; set; } 
        public Status TrangThai { get; set; } 

        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        // Quan hệ 1-1
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
