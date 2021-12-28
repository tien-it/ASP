using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace doanasp.Models
{
    public class HoaDon
<<<<<<< Updated upstream
    {
        [Key]
        [DisplayName("MÃ HÓA ĐƠN")]
        public int MAHOADON { get; set; }
        [DisplayName("MÃ KHÁCH HÀNG")]
        public int MAKHACHHANG { get; set; }
        [DisplayName("SỐ ĐIỆN THOẠI")]
        public String SDT { get; set; }
        [DisplayName("ĐỊA CHỈ")]
        public String DIACHI { get; set; }
        [DisplayName("GHI CHÚ")]
        public String GHICHU { get; set; }
        [DisplayName("NGÀY LẬP")]
        public DateTime NGAYLAP { get; set; }
        [DisplayName("CẬP NHẬT")]
        public DateTime NGAYGIAO { get; set; }
=======
    {  
        [Key]
        [DisplayName("MÃ HÓA ĐƠN")]
        public string MAHOADON { get; set; }
        
        [DisplayName("MÃ KHÁCH HÀNG")]
        public account MAKHACHHANG { get; set; }
        
        [DisplayName("ĐỊA CHỈ")]
        public string DIACHI { get; set; }
        [DisplayName("SỐ ĐIỆN THOẠI")]
        public string SDT { get; set; }
        
        [DisplayName("GHI CHÚ")]
        public string GHICHU { get; set; }
        
        [DisplayName("NGÀY LẬP")]
        public DateTime NGAYLAP { get; set; }
        
        [DisplayName("NGÀY GIAO")]
        public DateTime NGAYGIAO { get; set; }
        
>>>>>>> Stashed changes
        [DisplayName("TỔNG TIỀN")]
        public int TONGTIEN { get; set; }
        //trang thái hóa đơn : 0 đang xử lý , 1 đã tiếp nhận, 2 đang giao, 4 là giao hàng thành công, -1 bị hủy 
        [DisplayName("TRẠNG THÁI")]
        public int TRANGTHAI { get; set; }
    }
}
