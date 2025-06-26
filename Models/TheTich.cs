using System.ComponentModel.DataAnnotations;

namespace PerfumeShop.Models
{
	public class TheTich
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Ten { get; set; }
		public virtual List<SanPhamChiTiet> SanPhamChiTiets { get; set; } = new List<SanPhamChiTiet> ();
	}
}
