using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace doanasp.Migrations
{
    public partial class addcallmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    makhachhang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tendangnhap = table.Column<string>(nullable: true),
                    matkhau = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    hovaten = table.Column<string>(nullable: true),
                    diachi = table.Column<string>(nullable: true),
                    sodienthoai = table.Column<string>(nullable: true),
                    phuongthucdangnhap = table.Column<string>(nullable: true),
                    trangthai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.makhachhang);
                });

            migrationBuilder.CreateTable(
                name: "loaiSanPhams",
                columns: table => new
                {
                    maloai = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tensanpham = table.Column<string>(nullable: true),
                    trangthai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiSanPhams", x => x.maloai);
                });

            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    mahoadon = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    makhachhang = table.Column<int>(nullable: false),
                    diachi = table.Column<string>(nullable: true),
                    ghichu = table.Column<string>(nullable: true),
                    ngaylap = table.Column<DateTime>(nullable: false),
                    capnhat = table.Column<DateTime>(nullable: false),
                    tongtien = table.Column<int>(nullable: false),
                    trangthai = table.Column<int>(nullable: false),
                    accountmakhachhang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.mahoadon);
                    table.ForeignKey(
                        name: "FK_hoaDons_Accounts_accountmakhachhang",
                        column: x => x.accountmakhachhang,
                        principalTable: "Accounts",
                        principalColumn: "makhachhang",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sanPhams",
                columns: table => new
                {
                    masanpham = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maloai1 = table.Column<int>(nullable: true),
                    tensanpham = table.Column<string>(nullable: true),
                    giaban = table.Column<string>(nullable: true),
                    soluongton = table.Column<int>(nullable: false),
                    motasanpham = table.Column<string>(nullable: true),
                    mausac = table.Column<string>(nullable: true),
                    kichthuoc = table.Column<string>(nullable: true),
                    chatlieu = table.Column<string>(nullable: true),
                    phong = table.Column<string>(nullable: true),
                    noisanxuat = table.Column<string>(nullable: true),
                    hinhanh = table.Column<string>(nullable: true),
                    trangthai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhams", x => x.masanpham);
                    table.ForeignKey(
                        name: "FK_sanPhams_loaiSanPhams_maloai1",
                        column: x => x.maloai1,
                        principalTable: "loaiSanPhams",
                        principalColumn: "maloai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "chiTietHoaDons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mahoadon1 = table.Column<int>(nullable: true),
                    masanpham1 = table.Column<int>(nullable: true),
                    diachi = table.Column<string>(nullable: true),
                    ghichu = table.Column<string>(nullable: true),
                    ngaylap = table.Column<DateTime>(nullable: false),
                    capnhat = table.Column<DateTime>(nullable: false),
                    tongtien = table.Column<int>(nullable: false),
                    trangthai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietHoaDons", x => x.id);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_hoaDons_mahoadon1",
                        column: x => x.mahoadon1,
                        principalTable: "hoaDons",
                        principalColumn: "mahoadon",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chiTietHoaDons_sanPhams_masanpham1",
                        column: x => x.masanpham1,
                        principalTable: "sanPhams",
                        principalColumn: "masanpham",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gioHangs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    masanpham1 = table.Column<int>(nullable: true),
                    makhachhang1 = table.Column<int>(nullable: true),
                    soluong = table.Column<string>(nullable: true),
                    trangthai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangs", x => x.id);
                    table.ForeignKey(
                        name: "FK_gioHangs_Accounts_makhachhang1",
                        column: x => x.makhachhang1,
                        principalTable: "Accounts",
                        principalColumn: "makhachhang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_gioHangs_sanPhams_masanpham1",
                        column: x => x.masanpham1,
                        principalTable: "sanPhams",
                        principalColumn: "masanpham",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_mahoadon1",
                table: "chiTietHoaDons",
                column: "mahoadon1");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietHoaDons_masanpham1",
                table: "chiTietHoaDons",
                column: "masanpham1");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangs_makhachhang1",
                table: "gioHangs",
                column: "makhachhang1");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangs_masanpham1",
                table: "gioHangs",
                column: "masanpham1");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_accountmakhachhang",
                table: "hoaDons",
                column: "accountmakhachhang");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhams_maloai1",
                table: "sanPhams",
                column: "maloai1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietHoaDons");

            migrationBuilder.DropTable(
                name: "gioHangs");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "sanPhams");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "loaiSanPhams");
        }
    }
}
