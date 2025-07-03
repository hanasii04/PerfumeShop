using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.DTO
{
    public class MaGiamGiaDTO : IValidatableObject
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Tên mã giảm giá là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên mã giảm giá không được vượt quá 50 ký tự.")]
        public string TenMaGiamGia { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm là bắt buộc.")]
        [Range(1, 100, ErrorMessage = "Phần trăm giảm phải từ 1 đến 100.")]
        public int PhanTramGiam { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime NgayKetThuc { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int SoLuong { get; set; }

        public bool TrangThai { get; set; } = true;

        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // ✅ Ngày kết thúc phải >= ngày bắt đầu
            if (NgayKetThuc < NgayBatDau)
            {
                yield return new ValidationResult(
                    "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.",
                    new[] { nameof(NgayKetThuc) });
            }

            // ✅ Khi tạo mới, ngày bắt đầu không được ở quá khứ
            if (Id == null && NgayBatDau.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Ngày bắt đầu không được ở trong quá khứ.",
                    new[] { nameof(NgayBatDau) });
            }

            // ✅ Nếu mã đang hoạt động, số lượng phải > 0
            if (TrangThai && SoLuong <= 0)
            {
                yield return new ValidationResult(
                    "Mã giảm giá đang hoạt động phải có số lượng lớn hơn 0.",
                    new[] { nameof(SoLuong) });
            }
        }
    }
}
