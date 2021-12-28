using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace doanasp.Models
{
    public class QLBH
    {
        [Key]
        [DisplayName("MÃ CHI TIẾT SẢN PHẨM ")]
        public String MACHITIETSANPHAM { get; set; }

        [DisplayName("NGÀY NHẬP ")]
        public String NGAYNHAP { get; set; }

        [DisplayName("SỐ LƯỢNG NHẬP")]
        public int SOLUONGNHAP { get; set; }

        [DisplayName("ĐƠN GIÁ")]
        public int DONGIA { get; set; }
        [DisplayName("SỐ LƯỢNG TỒN CŨ")]
        public int SOLUONGTONCU { get; set; }
        [DisplayName("SỐ LƯỢNG TỒN MỚI")]
        public int SOLUONGTONMOI { get; set; }
        [DisplayName("GIÁ BÁN")]
        public int GIABAN { get; set; }
        [DisplayName("TRANGTHAI")]
        public int TRANGTHAI { get; set; }
    }
}
