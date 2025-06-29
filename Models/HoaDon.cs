using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public enum OrderStatus { ChoXacNhan, ChoLayHang, DangGiaoHang, DaGiaoHang, DaHuy }
	public class HoaDon 
	{
		[Key]
		public int Id { get; set; }

		public string MaHoaDon { get; set; }
        [Required]
		[Range(0, int.MaxValue)]
		public decimal TongTien { get; set; }
		public OrderStatus TrangThai { get; set; } = OrderStatus.ChoXacNhan;
		public DateTime NgayTao { get; set; } = DateTime.Now;
		[ForeignKey("PhuongThucVanChuyen")]
		public int? Id_PhuongThucVanChuyen { get; set; }
		public virtual PhuongThucVanChuyen PhuongThucVanChuyen { get; set; }
		[ForeignKey("PhuongThucThanhToan")]
		public int? Id_PhuongThucThanhToan { get; set; }
		public virtual PhuongThucThanhToan PhuongThucThanhToan { get; set; }
		[ForeignKey("DiaChi")]
		public int? Id_DiaChi { get; set; }
		public virtual DiaChi DiaChi { get; set; }
		[ForeignKey("MaGiamGia")]
		public int? Id_MaGiamGia { get; set; }
		public virtual MaGiamGia MaGiamGia { get; set; }
		[ForeignKey("NguoiDung")]
		public int Id_NguoiDung { get; set; }
		public virtual NguoiDung? NguoiDung { get; set; }
		public virtual List<HoaDonChiTiet> HoaDonChiTiets { get; set; } = new List<HoaDonChiTiet>();
	}
}
