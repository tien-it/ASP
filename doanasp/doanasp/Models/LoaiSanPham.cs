using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace doanasp.Models
{
    public class LoaiSanPham
    {   //mã loại sản phẩm, khóa chính 
        [Key]
        [DisplayName("MÃ LOẠI SẢN PHẨM")]
        public String MALOAISP { get; set; }

        //địa tên sản phẩm string
        [DisplayName("TÊN LOẠI SẢN PHẨM")]
        public String TENLOAISP { get; set; }

        //trang thái sản phẩm 0 loại sản phẩm k có sản phẩm. 1 là loại sản phẩm có ít nhất 1 sản phẩm
        [DisplayName("TRẠNG THÁI")]
        public int trangthai { get; set; }
        public List<SanPham> sanPhams { get; set; }
    }
}
