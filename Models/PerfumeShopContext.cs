using Microsoft.EntityFrameworkCore;

namespace PerfumeShop.Models
{
	public class PerfumeShopContext : DbContext
	{
		public PerfumeShopContext(DbContextOptions<PerfumeShopContext> options) : base(options) { }

		public DbSet<TaiKhoan> TaiKhoans { get; set; }
		public DbSet<NguoiDung> NguoiDungs { get; set; }
		public DbSet<GioHang> GioHangs { get; set; }
		public DbSet<GioHangChiTiet> GioHangChiTiets { get; set; }
		public DbSet<SanPham> SanPhams { get; set; }
		public DbSet<SanPhamChiTiet> SanPhamChiTiets { get; set; }
		public DbSet<TheTich> TheTichs { get; set; }
		public DbSet<HoaDon> HoaDons { get; set; }
		public DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
		public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
		public DbSet<PhuongThucVanChuyen> PhuongThucVanChuyens { get; set; }
		public DbSet<DiaChi> DiaChis { get; set; }
		public DbSet<MaGiamGia> MaGiamGias { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TaiKhoan>().HasIndex(t => t.Username).IsUnique();
			modelBuilder.Entity<NguoiDung>().HasIndex(n => n.Email).IsUnique();
			modelBuilder.Entity<MaGiamGia>().HasIndex(m => m.TenMaGiamGia).IsUnique();
		}

		public override int SaveChanges()
		{
			var creatableEntries = ChangeTracker.Entries<ICreatable>();
			foreach (var entry in creatableEntries)
			{
				if (entry.State == EntityState.Added)
					entry.Entity.NgayTao = DateTime.Now;
			}

			var updatableEntries = ChangeTracker.Entries<IUpdatable>();
			foreach (var entry in updatableEntries)
			{
				if (entry.State == EntityState.Modified)
					entry.Entity.NgayCapNhat = DateTime.Now;
			}

			return base.SaveChanges();
		}
	}
}
