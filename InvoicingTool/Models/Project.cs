using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicingTool.Models
{
    public class Project
    {
        public int ID { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}