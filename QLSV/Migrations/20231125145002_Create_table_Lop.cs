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
            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    Malop = table.Column<string>(type: "TEXT", nullable: false),
                    Tenlop = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.Malop);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lop");
        }
    }
}
