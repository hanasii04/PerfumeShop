using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class HoaDonChiTiet
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int SoLuong { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int DonGia { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int TongTien { get; set; }
		[ForeignKey("SanPhamChiTiet")]
		public int Id_SanPhamChiTiet { get; set; }
		public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
		[ForeignKey("HoaDon")]
		public int Id_HoaDon { get; set; }
		public virtual HoaDon HoaDon { get; set; }
	}
}
