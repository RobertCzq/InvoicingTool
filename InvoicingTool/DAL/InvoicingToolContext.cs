using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoicingTool.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InvoicingTool.DAL
{
    public class InvoicingToolContext : DbContext
    {
        public InvoicingToolContext() : base("DefaultConnection")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}