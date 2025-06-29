using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public enum Gender { Nam, Nu, Unisex}
	public class SanPham 
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
		[Required]
		[StringLength(100)]
		public string ThuongHieu { get; set; }
		public Gender GioiTinh { get; set; }
		
		[Required]
		public string MoTa { get; set; }
		[Required]
		[StringLength(500)]
		public string? HinhAnh { get; set; }
		public virtual List<SanPhamChiTiet> SanPhamChiTiets { get; set; } = new List<SanPhamChiTiet>();
	}
}
