using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicingTool.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public string UserEmail { get; set; }
        public int ProjectID { get; set; }
        public int CustomerID { get; set; }
        [Required]
        public string InvoiceTitle { get; set; }
        public string InvoiceDescription { get; set; }
        public decimal Hours { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Project Project { get; set; }
    }
}