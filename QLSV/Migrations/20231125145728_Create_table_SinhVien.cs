using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_SinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "TEXT", nullable: false),
                    Hovaten = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Malop = table.Column<string>(type: "TEXT", nullable: true),
                    Makhoa = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SinhVien_Khoa_Makhoa",
                        column: x => x.Makhoa,
                        principalTable: "Khoa",
                        principalColumn: "Makhoa");
                    table.ForeignKey(
                        name: "FK_SinhVien_Lop_Malop",
                        column: x => x.Malop,
                        principalTable: "Lop",
                        principalColumn: "Malop");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_Makhoa",
                table: "SinhVien",
                column: "Makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_Malop",
                table: "SinhVien",
                column: "Malop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhVien");
        }
    }
}
