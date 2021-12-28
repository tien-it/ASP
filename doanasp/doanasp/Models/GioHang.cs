using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace doanasp.Models
{
    public class GioHang
    {
        public int id{ get; set; }
        //mã  sản phẩm

        [DisplayName("MÃ SẢN PHẨM")]
        public SanPham MACHITIETSANPHAM { get; set; }
        //mã khách hàng 
        
        [DisplayName("MÃ KHÁCH HÀNG")]
        public account MAKHACHHANG { get; set; }
        //số lượng
        [DisplayName("SỐ LƯỢNG")]
        public int SOLUONG { get; set; }
        //trạng thái  1 là còn sản phẩm . -1 là hết sản phẩm 
        [DisplayName("TRẠNG THÁI")]
        public int TRANGTHAI { get; set; }
        
    }
}
