using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class GioHangChiTiet
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int SoLuong { get; set; }
		[ForeignKey("GioHang")]
		public int Id_GioHang { get; set; }
		public virtual GioHang? GioHang { get; set; }
		[ForeignKey("SanPhamChiTiet")]
		public int Id_SanPhamChiTiet { get; set; }
		public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
	}
}
