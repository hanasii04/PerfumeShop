using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public enum Gender { Nam, Nu, Unisex}
	public class SanPham : IUpdatable
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		public string TenSanPham { get; set; }
		[Required]
		[StringLength(100)]
		public string ThuongHieu { get; set; }
		[StringLength(100)]
		public string QuocGia { get; set; }
		public Gender GioiTinh { get; set; }
		[Range(0, int.MaxValue)]
		public int ThoiGianLuuHuong { get; set; }
		public string MoTa { get; set; }
		[StringLength(500)]
		public string HinhAnh { get; set; }
		public DateTime NgayTao { get; set; } = DateTime.Now;
		public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
		public virtual List<SanPhamChiTiet> SanPhamChiTiets { get; set; } = new List<SanPhamChiTiet>();
	}
}
