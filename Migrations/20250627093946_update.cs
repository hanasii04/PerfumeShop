using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NguoiDungs_Username",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "NguoiDungs");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "NguoiDungs");

            migrationBuilder.RenameColumn(
                name: "VaiTro",
                table: "NguoiDungs",
                newName: "ID_TK");

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    ID_TK = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.ID_TK);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NguoiDungs_ID_TK",
                        column: x => x.ID_TK,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_UserName",
                table: "TaiKhoans",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.RenameColumn(
                name: "ID_TK",
                table: "NguoiDungs",
                newName: "VaiTro");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "NguoiDungs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "NguoiDungs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "NguoiDungs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Username",
                table: "NguoiDungs",
                column: "Username",
                unique: true);
        }
    }
}
