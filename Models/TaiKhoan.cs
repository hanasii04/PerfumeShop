using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
    public enum Role { QuanLy, NhanVien, KhachHang }
    public enum Status { HoatDong, KhongHoatDong }

    public class TaiKhoan
    {
        [Key]
        public int ID_TK { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        public Role VaiTro { get; set; } = Role.KhachHang;
        public Status TrangThai { get; set; } = Status.HoatDong;

        // Quan hệ 1-1
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
