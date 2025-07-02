using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeShop.Migrations
{
    /// <inheritdoc />
    public partial class jjjj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NguoiDungs_ID_TK",
                table: "NguoiDungs");

            migrationBuilder.AlterColumn<int>(
                name: "ID_TK",
                table: "NguoiDungs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_ID_TK",
                table: "NguoiDungs",
                column: "ID_TK",
                unique: true,
                filter: "[ID_TK] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NguoiDungs_ID_TK",
                table: "NguoiDungs");

            migrationBuilder.AlterColumn<int>(
                name: "ID_TK",
                table: "NguoiDungs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_ID_TK",
                table: "NguoiDungs",
                column: "ID_TK",
                unique: true);
        }
    }
}
