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
    public class  Comment
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime IssueDate { get; set; }
        public String Content { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [DataType(DataType.ImageUrl)]
        public String Image { get; set; }
        public bool status { get; set; }

    }
}
