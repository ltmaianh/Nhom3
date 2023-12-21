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
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "SinhVien",
                newName: "DiaChi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiaChi",
                table: "SinhVien",
                newName: "Address");
        }
    }
}
