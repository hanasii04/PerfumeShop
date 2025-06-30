using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
    public class MaGiamGia : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenMaGiamGia { get; set; }

        [Required]
        [Range(0, 100)]
        public int PhanTramGiam { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int SoLuong { get; set; }

        public bool TrangThai { get; set; } = true;

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; }

        public virtual List<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

        // ✅ Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NgayKetThuc < NgayBatDau)
            {
                yield return new ValidationResult(
                    "Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.",
                    new[] { nameof(NgayKetThuc) });
            }
        }
    }
}
