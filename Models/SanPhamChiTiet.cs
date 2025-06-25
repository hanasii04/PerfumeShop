using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class SanPhamChiTiet
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int SoLuong { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public decimal GiaBan { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public decimal GiaNhap { get; set; }
		[ForeignKey("Id_SanPham")]
		public int Id_SanPham { get; set; }
		public virtual SanPham SanPham { get; set; }
		[ForeignKey("Id_TheTich")]
		public int Id_TheTich { get; set; }
		public virtual TheTich TheTich { get; set; }
		public virtual List<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();
		public virtual List<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
	}
}
