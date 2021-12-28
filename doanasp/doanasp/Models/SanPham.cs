using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace doanasp.Models
{
    public class SanPham
    {
        [Key]
        [DisplayName("MÃ SẢN PHẨM")]
        public String MASANPHAM { get; set; }

        [DisplayName("MÃ LOẠI SẢN PHẨM ")]
        public String MALOAISANPHAM { get; set; }

        [DisplayName("TÊN SẢN PHẨM ")]
        public String TENSANPHAM { get; set; }

        [DisplayName("MÔ TẢ")]
        public String MOTA { get; set; }

        [DisplayName("HÌNH ẢNH")]
        public String HINHANH{ get; set; }

        [DisplayName("TRANGTHAI")]
        public int TRANGTHAI { get; set; }
        
    }
}
