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
        public int id { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string username { get; set; }
        [DisplayName("Mật Khẩu")]
        public String password { get; set; }
        [DisplayName("Email")]
        public String email { get; set; }
        [DisplayName("Họ và Tên")]
        public String fullname { get; set; }
        [DisplayName("Địa Chỉ")]
        public String permanent_address { get; set; }
        [DisplayName("SĐT")]
        public String phonenumber { get; set; }
        [DisplayName("Ảnh")]
        public String avatar { get; set; }
        [DisplayName("Admininstrator")]
        public int type_user { get; set; }
    }
}
