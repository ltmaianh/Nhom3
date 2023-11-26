using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Diem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "TEXT", nullable: false),
                    TenSV = table.Column<string>(type: "TEXT", nullable: true),
                    TenMH = table.Column<string>(type: "TEXT", nullable: true),
                    DiemMH = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diem", x => x.MaSV);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diem");
        }
    }
}
