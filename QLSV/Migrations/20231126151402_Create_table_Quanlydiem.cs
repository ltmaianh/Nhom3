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
                    Tenmonhoc = table.Column<string>(type: "TEXT", nullable: true),
                    Diem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quanlydiem", x => x.Sothutu);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quanlydiem");
        }
    }
}
