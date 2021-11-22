using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using doanasp.Models;
namespace doanasp.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { }
        public DbSet<account> Accounts { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<LoaiSanPham> loaiSanPhams { get; set; }
        public DbSet<ChiTietHoaDon> chiTietHoaDons { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        
        
       

    }
}
