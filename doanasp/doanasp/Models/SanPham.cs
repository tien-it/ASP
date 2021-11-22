using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace doanasp.Models
{
    public class SanPham
    {   //mã  sản phẩm khóa chính
        [Key]
        [DisplayName("MÃ SẢN PHẨM")]
        public int masanpham { get; set; }
        //mã loại sản phẩm khóa ngoại account
        [DisplayName("MÃ LOẠI")]
        public LoaiSanPham maloai { get; set; }
        //địa tên sản phẩm string
        [DisplayName("TÊN SẢN PHẨM")]
        public String tensanpham { get; set; }
        //giá bán string 
        [DisplayName("GÍA BÁN")]
        public String giaban{ get; set; }
        //số lượng tồn int 
        [DisplayName("SỐ LƯỢNG TỒN")]
        public int soluongton { get; set; }
        //mô tả sản phẩm 
        [DisplayName("MÔ TẢ")]
        public String motasanpham { get; set; }
        //màu sắc
        [DisplayName("MÀU SẮC")]
        public String mausac { get; set; }
        //kichthuoc
        [DisplayName("KÍCH THƯỚC")]
        public String kichthuoc { get; set; }
        //chất liệu
        [DisplayName("CHẤT LIỆU")]
        public String chatlieu { get; set; }
        //phòng
        [DisplayName("PHÒNG")]
        public String phong { get; set; }
        //nơi sản xuất
        [DisplayName("NƠI SẢN XUẤT")]
        public String noisanxuat { get; set; }
        //tổng tiền của hóa đơn
        [DisplayName("HÌNH ẢNH")]
        public String hinhanh { get; set; }
        //trang thái sản phẩm 0 , hết hàng , 1 còn hàng. -1 tạm khóa
        [DisplayName("TRẠNG THÁI")]
        public int trangthai { get; set; }
    }
}
