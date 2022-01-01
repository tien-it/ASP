using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace doanasp.Models
{
    public class ProductType
    {
        [Key]
        [DisplayName("Mã loại sản phẩm")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Tên loại sản phẩm")]
        public string TypeName { get; set; }

        [DisplayName("Trạng thái")]
        public int Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
