using PerfumeShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class GioHang
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("NguoiDung")]
		public int Id_NguoiDung { get; set; }
		public virtual NguoiDung NguoiDung { get; set; }
		public virtual List<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();
	}
}
