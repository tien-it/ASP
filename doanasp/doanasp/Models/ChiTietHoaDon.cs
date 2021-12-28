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
<<<<<<< Updated upstream
            public int id { get; set; }
            
            [DisplayName("MÃ HÓA ĐƠN")]
            public HoaDon MAHOADON { get; set; }
            [DisplayName("MÃ SẢN PHẨM")]
            public SanPham MACHITIETSANPHAM { get; set; }
            [DisplayName("SỐ LƯỢNG")]
            public int SOLUONG { get; set; }
            [DisplayName("ĐƠN GIÁ")]
            public int DONGIA { get; set; }
            [DisplayName("THÀNH TIỀN")]
            public int THANHTIEN { get; set; }
            //trang thái hóa đơn : 0 đang xử lý , 1 đã tiếp nhận, 2 đang giao, 4 là giao hàng thành công, -1 bị hủy 
            [DisplayName("TRẠNG THÁI")]
            public int TRANGTHAI { get; set; }
       
=======
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
>>>>>>> Stashed changes
    }
}
