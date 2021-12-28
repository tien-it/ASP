using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace doanasp.Models
{
    public class ChiTietHoaDon
    {

        [Key]
        [DisplayName("Mã Hóa Đơn")]
        public HoaDon MAHOADON { get; set; }
        [DisplayName("Mã Chi Tiết Sản Phẩm")]
        public ChiTietSanPham MACHITIETSANPHAM { get; set; }
        [DisplayName("Số Lượng")]
        public int SOLUONG { get; set; }
        [DisplayName("Đơn GIá")]
        public int DONGIA { get; set; }
        [DisplayName("Thành Tiền")]
        public int THANHTIEN { get; set; }
        [DisplayName("Trạng Thái ")]
        public int TRANGTHAI { get; set; }
    }
}
