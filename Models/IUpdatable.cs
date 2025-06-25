namespace PerfumeShop.Models
{
	public interface IUpdatable : ICreatable
	{
		DateTime? NgayCapNhat { get; set; }
	}
	// Giảm lặp code ở các model có thuộc tính NgayTao và NgayCapNhat
}
