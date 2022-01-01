using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace doanasp.Models
{
    public class Invoice
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Code")]
        public string Code { get; set; }
        [DisplayName("IssuedDate")]
        public DateTime IssueDate { get; set; }
        [DisplayName("Shipping Address")]
        public string ShippingAddress { get; set; }
        [DisplayName("Shipping Phone")]
        public string ShippingPhone { get; set; }
        [DisplayName("Total")]
        public int Total { get; set; }
        [DisplayName("Account ID")]
        public Account Account { get; set; }
        public int AccountId { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
