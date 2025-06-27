using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.DTO
{
    public class DangKyVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        [Phone]
        public string SoDienThoai { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
