using Microsoft.EntityFrameworkCore;
using PerfumeShop.DTO;

namespace PerfumeShop.Models
{
    public class PerfumeShopContext : DbContext
    {
        public PerfumeShopContext(DbContextOptions<PerfumeShopContext> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
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
            // Unique constraint
            modelBuilder.Entity<TaiKhoan>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<NguoiDung>().HasIndex(n => n.Email).IsUnique();
            modelBuilder.Entity<MaGiamGia>().HasIndex(m => m.TenMaGiamGia).IsUnique();

            // Quan hệ 1-1: NguoiDung → TaiKhoan
            modelBuilder.Entity<NguoiDung>()
                .HasOne(nd => nd.TaiKhoan)
                .WithOne(tk => tk.NguoiDung)
                .HasForeignKey<NguoiDung>(nd => nd.ID_TK)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-1: NguoiDung - GioHang
            modelBuilder.Entity<NguoiDung>()
                .HasOne(n => n.GioHang)
                .WithOne(g => g.NguoiDung)
                .HasForeignKey<GioHang>(g => g.Id_NguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            // 1-n: NguoiDung - HoaDon
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NguoiDung)
                .WithMany(n => n.HoaDons)
                .HasForeignKey(h => h.Id_NguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.DiaChi)
                .WithMany(d => d.HoaDons)
                .HasForeignKey(h => h.Id_DiaChi)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.MaGiamGia)
                .WithMany(m => m.HoaDons)
                .HasForeignKey(h => h.Id_MaGiamGia)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.PhuongThucThanhToan)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(h => h.Id_PhuongThucThanhToan)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.PhuongThucVanChuyen)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(h => h.Id_PhuongThucVanChuyen)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DiaChi>()
                .HasOne(d => d.NguoiDung)
                .WithMany(n => n.DiaChis)
                .HasForeignKey(d => d.Id_NguoiDung)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.GioHang)
                .WithMany(g => g.GioHangChiTiets)
                .HasForeignKey(g => g.Id_GioHang)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GioHangChiTiet>()
                .HasOne(g => g.SanPhamChiTiet)
                .WithMany(s => s.GioHangChiTiets)
                .HasForeignKey(g => g.Id_SanPhamChiTiet)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HoaDonChiTiet>()
                .HasOne(h => h.HoaDon)
                .WithMany(h => h.HoaDonChiTiets)
                .HasForeignKey(h => h.Id_HoaDon)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HoaDonChiTiet>()
                .HasOne(h => h.SanPhamChiTiet)
                .WithMany(s => s.HoaDonChiTiets)
                .HasForeignKey(h => h.Id_SanPhamChiTiet)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SanPhamChiTiet>()
                .HasOne(s => s.SanPham)
                .WithMany(s => s.SanPhamChiTiets)
                .HasForeignKey(s => s.Id_SanPham)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SanPhamChiTiet>()
                .HasOne(s => s.TheTich)
                .WithMany(t => t.SanPhamChiTiets)
                .HasForeignKey(s => s.Id_TheTich)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-0O61DM6\\TRUNGTT;Database=PerfumeShop;Trusted_Connection=True;TrustServerCertificate=True");
                
        }
        public DbSet<PerfumeShop.DTO.MaGiamGiaDTO> MaGiamGiaDTO { get; set; } = default!;
    }
}
