using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class HoaDonChiTiet
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public int SoLuong { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public decimal DonGia { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public decimal TongTien { get; set; }
		[ForeignKey("Id_SanPhamChiTiet")]
		public int Id_SanPhamChiTiet { get; set; }
		public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
		[ForeignKey("Id_HoaDon")]
		public int Id_HoaDon { get; set; }
		public virtual HoaDon HoaDon { get; set; }
	}
}
