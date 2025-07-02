using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
    public class NguoiDung
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; }

        public DateTime? NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; }

        // Foreign Key & navigation 1-1
        [ForeignKey("TaiKhoan")]
        public int? ID_TK { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }

        // Quan hệ khác
        public virtual List<HoaDon> HoaDons { get; set; } = new();
        public virtual List<DiaChi> DiaChis { get; set; } = new();
        public virtual GioHang GioHang { get; set; }
    }
}

