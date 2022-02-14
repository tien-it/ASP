using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace doanasp.Models
{
    public class Product
    {
        [Key]
        [DisplayName("Product ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("SKU")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Author")]
        public string Author { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống!")]
        [DisplayName("Stock")]
        public int Stock { get; set; }


        public int ProductTypeId { get; set; }
        [DisplayName("ProductType")]
        public ProductType ProductType { get; set; }

        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public List<Cart> carts { get; set; }
    }
}
