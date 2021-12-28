using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doanasp.Models
{
    public class QuanLyBanHang
    {
        [Key]
        [DisplayName("Mã Chi Tiết Sản Phẩm ")]
        public ChiTietSanPham MACHITIETSANPHAM { get; set; }
        [DisplayName("Ngày Nhập")]
        public DateTime NGAYNHAP { get; set; }
        [DisplayName("Số Lượng Nhập")]
        public int SOLUONGNHAP { get; set; }
        [DisplayName("Đơn Giá")]
        public int DONGIA { get; set; }
        [DisplayName("So Lượng Tồn Cũ")]
        public int SOLUONGTONCU { get; set; }
        [DisplayName("Số Lượng Tồn Mới")]
        public int SOLUONGTONMOI { get; set; }
        [DisplayName("Giá Bán")]
        public int GIABAN { get; set; }
        [DisplayName("Trạng Thái")]
        public int TRANGTHAI { get; set; }    
    }
}
