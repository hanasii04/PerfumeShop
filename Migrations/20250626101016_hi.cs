using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Migrations
{
    /// <inheritdoc />
    public partial class hi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaGiamGias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMaGiamGia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaGiamGias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucVanChuyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GiaTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucVanChuyens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ThuongHieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuocGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    ThoiGianLuuHuong = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheTichs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheTichs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaChis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Id_NguoiDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaChis_NguoiDungs_Id_NguoiDung",
                        column: x => x.Id_NguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_NguoiDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangs_NguoiDungs_Id_NguoiDung",
                        column: x => x.Id_NguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<int>(type: "int", nullable: false),
                    GiaNhap = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_SanPham = table.Column<int>(type: "int", nullable: false),
                    Id_TheTich = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_SanPhams_Id_SanPham",
                        column: x => x.Id_SanPham,
                        principalTable: "SanPhams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_TheTichs_Id_TheTich",
                        column: x => x.Id_TheTich,
                        principalTable: "TheTichs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_PhuongThucVanChuyen = table.Column<int>(type: "int", nullable: false),
                    Id_PhuongThucThanhToan = table.Column<int>(type: "int", nullable: false),
                    Id_DiaChi = table.Column<int>(type: "int", nullable: false),
                    Id_MaGiamGia = table.Column<int>(type: "int", nullable: true),
                    Id_NguoiDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDons_DiaChis_Id_DiaChi",
                        column: x => x.Id_DiaChi,
                        principalTable: "DiaChis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDons_MaGiamGias_Id_MaGiamGia",
                        column: x => x.Id_MaGiamGia,
                        principalTable: "MaGiamGias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_HoaDons_NguoiDungs_Id_NguoiDung",
                        column: x => x.Id_NguoiDung,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDons_PhuongThucThanhToans_Id_PhuongThucThanhToan",
                        column: x => x.Id_PhuongThucThanhToan,
                        principalTable: "PhuongThucThanhToans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDons_PhuongThucVanChuyens_Id_PhuongThucVanChuyen",
                        column: x => x.Id_PhuongThucVanChuyen,
                        principalTable: "PhuongThucVanChuyens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Id_GioHang = table.Column<int>(type: "int", nullable: false),
                    Id_SanPhamChiTiet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_GioHangs_Id_GioHang",
                        column: x => x.Id_GioHang,
                        principalTable: "GioHangs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GioHangChiTiets_SanPhamChiTiets_Id_SanPhamChiTiet",
                        column: x => x.Id_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<int>(type: "int", nullable: false),
                    Id_SanPhamChiTiet = table.Column<int>(type: "int", nullable: false),
                    Id_HoaDon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_HoaDons_Id_HoaDon",
                        column: x => x.Id_HoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiets_SanPhamChiTiets_Id_SanPhamChiTiet",
                        column: x => x.Id_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaChis_Id_NguoiDung",
                table: "DiaChis",
                column: "Id_NguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_Id_GioHang",
                table: "GioHangChiTiets",
                column: "Id_GioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiets_Id_SanPhamChiTiet",
                table: "GioHangChiTiets",
                column: "Id_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_Id_NguoiDung",
                table: "GioHangs",
                column: "Id_NguoiDung",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_Id_HoaDon",
                table: "HoaDonChiTiets",
                column: "Id_HoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiets_Id_SanPhamChiTiet",
                table: "HoaDonChiTiets",
                column: "Id_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_Id_DiaChi",
                table: "HoaDons",
                column: "Id_DiaChi");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_Id_MaGiamGia",
                table: "HoaDons",
                column: "Id_MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_Id_NguoiDung",
                table: "HoaDons",
                column: "Id_NguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_Id_PhuongThucThanhToan",
                table: "HoaDons",
                column: "Id_PhuongThucThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_Id_PhuongThucVanChuyen",
                table: "HoaDons",
                column: "Id_PhuongThucVanChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_MaGiamGias_TenMaGiamGia",
                table: "MaGiamGias",
                column: "TenMaGiamGia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Email",
                table: "NguoiDungs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Username",
                table: "NguoiDungs",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_Id_SanPham",
                table: "SanPhamChiTiets",
                column: "Id_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_Id_TheTich",
                table: "SanPhamChiTiets",
                column: "Id_TheTich");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GioHangChiTiets");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiets");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "DiaChis");

            migrationBuilder.DropTable(
                name: "MaGiamGias");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToans");

            migrationBuilder.DropTable(
                name: "PhuongThucVanChuyens");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "TheTichs");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
