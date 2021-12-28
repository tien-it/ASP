using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace doanasp.Models
{
    public class ChiTietSanPham
    {
        [Key]
        [DisplayName("MÃ SẢN PHẨM")]
        public String MASANPHAM { get; set; }

        [DisplayName("MÃ CHI TIẾT SẢN PHẨM ")]
        public String MACHITIETSANPHAM { get; set; }

        [DisplayName("THƯƠNG HIỆU ")]
        public String THUONGHIEU { get; set; }

        [DisplayName("NOI SẢN XUẤT")]
        public String NOISANXUAT { get; set; }

        [DisplayName("CHẤT LIỆU")]
        public String CHATLIEU { get; set; }
        [DisplayName("PHONG CÁCH")]
        public String PHONGCACH { get; set; }
        [DisplayName("MÀU SẮC")]
        public String MAUSAC { get; set; }


        [DisplayName("TRANGTHAI")]
        public int TRANGTHAI { get; set; }
    }
}
