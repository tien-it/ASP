using Microsoft.EntityFrameworkCore.Migrations;

namespace doanasp.Migrations
{
    public partial class haha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tongtien",
                table: "hoaDons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tongtien",
                table: "hoaDons");
        }
    }
}
