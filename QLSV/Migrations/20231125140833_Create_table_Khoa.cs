using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLSV.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Khoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    Makhoa = table.Column<string>(type: "TEXT", nullable: false),
                    Tenkhoa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.Makhoa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
