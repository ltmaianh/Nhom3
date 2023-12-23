using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Lop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Makhoa",
                table: "Lop",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lop_Makhoa",
                table: "Lop",
                column: "Makhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Lop_Khoa_Makhoa",
                table: "Lop",
                column: "Makhoa",
                principalTable: "Khoa",
                principalColumn: "Makhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lop_Khoa_Makhoa",
                table: "Lop");

            migrationBuilder.DropIndex(
                name: "IX_Lop_Makhoa",
                table: "Lop");

            migrationBuilder.DropColumn(
                name: "Makhoa",
                table: "Lop");
        }
    }
}
