using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(10)]
        public string SoDienThoai { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime? NgayCapNhat { get; set; }

        // Foreign Key & navigation 1-1
        [ForeignKey("TaiKhoan")]
        public int ID_TK { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }

        // Quan hệ khác
        public virtual List<HoaDon> HoaDons { get; set; } = new();
        public virtual List<DiaChi> DiaChis { get; set; } = new();
        public virtual GioHang GioHang { get; set; }
    }
}
