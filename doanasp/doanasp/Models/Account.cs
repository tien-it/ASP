using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace doanasp.Models
{
    public class Account
    {
        [Key]
        [DisplayName("MÃ TÀI KHOẢN")]
        public String MAKHACHHANG { get; set; }
        [DisplayName("HỌ VÀ TÊN ")]
        public String HOVATEN { get; set; }
        [DisplayName("MẬT KHẨU")]
        public String MATKHAU { get; set; }
        [DisplayName("SỐ ĐIỆN THOẠI")]
        public String SODIENTHOAI { get; set; }
        [DisplayName("EMAIL")]
        public String EMAIL { get; set; }
        [DisplayName("ĐỊA CHỈ")]
        public String DIACHI { get; set; }
        [DisplayName("GIỚI TÍNH")]
        public String GIOITINH { get; set; }
        [DisplayName("NGÀY SINH")]
        public DateTime NGAYSINH { get; set; }
        [DisplayName("LÀ ADMIN")]
        public bool ISADMIN { get; set; }
        [DisplayName("Trạng Thái ")]
        public int TRANGTHAI { get; set; }
    }
}
