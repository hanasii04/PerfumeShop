using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{

    public class TaiKhoan
    {
        [Key]
        public int Id  { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string VaiTro { get; set; }  = "KhachHang"; // Mặc định là Khách hàng, có thể là Admin hoặc Nhân viên
        public int TrangThai { get; set; } 


        // Quan hệ 1-1
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}
