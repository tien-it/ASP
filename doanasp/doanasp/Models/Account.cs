using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace doanasp.Models
{
    public class account
    {  
        [Key]
        [DisplayName("MÃ KHÁCH HÀNG")]
        public int makhachhang { get; set; }
        [DisplayName("Tên đăng nhập")]
        public string tendangnhap { get; set; }
        [DisplayName("Mật Khẩu")]
        public String matkhau { get; set; }
        [DisplayName("Email")]
        public String email { get; set; }
        [DisplayName("Họ và Tên")]
        public String hovaten { get; set; }
        [DisplayName("Địa Chỉ")]
        public String diachi { get; set; }
        [DisplayName("SĐT")]
        public String sodienthoai { get; set; }
        [DisplayName("Phương Thức Đăng nhập")]
        public String phuongthucdangnhap { get; set; }
        [DisplayName("Loại")]
        public int trangthai { get; set; }
    }
}
