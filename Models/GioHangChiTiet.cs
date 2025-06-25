using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeShop.Models
{
	public class GioHangChiTiet
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Id_NguoiDung")]
		public int Id_NguoiDung { get; set; }
		public virtual NguoiDung NguoiDung { get; set; }
		public virtual List<GioHangChiTiet> GioHangChiTiets { get; set; } = new List<GioHangChiTiet>();
	}
}
