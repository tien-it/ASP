using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doanasp.Models
{
    public class ChiTietSanPham
    {
        [Key]
        [DisplayName("Mã San Phẩm")]
        public SanPham MASANPHAM { get; set; }
        [DisplayName("Mã Chi Tiết Sản Phẩm")]
        public string MACHITIETSANPHAM { get; set; }
        [DisplayName("Thương Hiệu")]
        public string THUONGHIEU { get; set; }
        [DisplayName("Nơi Sản Xuất ")]
        public string NOISANXUAT { get; set; }
        [DisplayName("Chất Liệu")]
        public string CHATLIEU { get; set; }
        [DisplayName("Phong Cách")]
        public string PHONGCACH { get; set; }
        [DisplayName("Màu Sắc")]
        public string MAUSAC { get; set; }
        [DisplayName("Chiều Dài")]
        public string CHIEUDAI { get; set; }
        [DisplayName("Chiều Rộng")]
        public string CHIEURONG { get; set; }
        [DisplayName("Trạng Thái ")]
        public int TRANGTHAI { get; set; }

    }
}
