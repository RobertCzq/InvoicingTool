using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicingTool.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}