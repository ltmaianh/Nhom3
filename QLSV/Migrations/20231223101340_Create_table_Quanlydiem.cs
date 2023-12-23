using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Quanlydiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quanlydiem",
                columns: table => new
                {
                    Sothutu = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaSV = table.Column<string>(type: "TEXT", nullable: false),
                    TenSV = table.Column<string>(type: "TEXT", nullable: false),
                    Mamonhoc = table.Column<string>(type: "TEXT", nullable: true),
                    DiemMH = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quanlydiem", x => x.Sothutu);
                    table.ForeignKey(
                        name: "FK_Quanlydiem_Quanlymonhoc_Mamonhoc",
                        column: x => x.Mamonhoc,
                        principalTable: "Quanlymonhoc",
                        principalColumn: "Mamonhoc");
                    table.ForeignKey(
                        name: "FK_Quanlydiem_SinhVien_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quanlydiem_Mamonhoc",
                table: "Quanlydiem",
                column: "Mamonhoc");

            migrationBuilder.CreateIndex(
                name: "IX_Quanlydiem_MaSV",
                table: "Quanlydiem",
                column: "MaSV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quanlydiem");
        }
    }
}
