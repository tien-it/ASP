using Microsoft.EntityFrameworkCore.Migrations;

namespace doanasp.Migrations
{
    public partial class deletongtien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tongtien",
                table: "hoaDons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tongtien",
                table: "hoaDons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
