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
            //mã hóa đơn INT
            [Key]
            [DisplayName("MÃ HÓA ĐƠN")]
            public HoaDon mahoadon { get; set; }
            //mã khách hàng INT
            [Key]
            [DisplayName("MÃ SẢN PHẨM")]
            public SanPham masanpham { get; set; }
            //địa chỉ string
            [DisplayName("SỐ LƯỢNG")]
            public String diachi { get; set; }
            //ghi chú string
            [DisplayName("GHI CHÚ")]
            public String? ghichu { get; set; }
            //ngày lập hóa đơn date time
            [DisplayName("NGÀY LẬP")]
            public DateTime ngaylap { get; set; }
            //ngày cập nhật trạng thái hóa đơn date time
            [DisplayName("CẬP NHẬT")]
            public DateTime capnhat { get; set; }
            //tổng tiền của hóa đơn
            [DisplayName("TỔNG TIỀN")]
            public int tongtien { get; set; }
            //trang thái hóa đơn : 0 đang xử lý , 1 đã tiếp nhận, 2 đang giao, 4 là giao hàng thành công, -1 bị hủy 
            [DisplayName("TRẠNG THÁI")]
            public int trangthai { get; set; }
       
    }
}
