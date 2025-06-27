using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.DTO
{
    public class DangNhapVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }
    }
}
