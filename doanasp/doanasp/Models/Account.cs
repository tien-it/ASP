using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace doanasp.Models
{
    public class Account
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("UserName")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters")]
        public string Username { get; set; }
        [DisplayName("PassWord")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Phone number")]
        [RegularExpression("0\\d{9}", ErrorMessage = "SĐT không hợp lệ")]
        public string Phone { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Full Name")]
        public string Fullname { get; set; }
        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [DisplayName("Avatar")]
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
