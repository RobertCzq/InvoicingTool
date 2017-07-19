using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoicingTool.Models;

namespace InvoicingTool.DAL
{
    public class InvoicingToolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<InvoicingToolContext>
    {
        protected override void Seed(InvoicingToolContext context)
        {

            var customers = new List<Customer>
                {
                new Customer{CustomerName="Alex Jones",Address="Oak road, 29, Copenhagen, 2100", Contact="Test contact", PhoneNumber="1234455556"},
                new Customer{CustomerName="Alex Nielsen",Address="Oak road, 29, Copenhagen, 2100", Contact="Test contact", PhoneNumber="2234455556"},
                new Customer{CustomerName="Tim Draker",Address="Oak road, 29, Copenhagen, 2100", Contact="Test contact", PhoneNumber="3234455556"}
                };

            customers.ForEach(customer => context.Customers.Add(customer));
            context.SaveChanges();

            var projects = new List<Project>
                {
                new Project{ProjectName="Project 1",},
                new Project{ProjectName="Project 2",},
                new Project{ProjectName="Project 3",}
                };

            projects.ForEach(project => context.Projects.Add(project));
            context.SaveChanges();

            var invoices = new List<Invoice>
                {
                new Invoice{UserEmail="test@test.com",ProjectID=1,CustomerID=1, InvoiceTitle="Test1", InvoiceDescription="Test despcription 1", Hours=1.5m, Amount=1.2m, InvoiceDate = DateTime.Parse("2017-07-17"),  DueDate = DateTime.Parse("2017-07-30")},
                new Invoice{UserEmail="test@test.com",ProjectID=2,CustomerID=2, InvoiceTitle="Test2", InvoiceDescription="Test despcription 2", Hours=1.5m, Amount=1.2m, InvoiceDate = DateTime.Parse("2017-07-17"),  DueDate = DateTime.Parse("2017-07-30")},
                new Invoice{UserEmail="test@test.com",ProjectID=3,CustomerID=3, InvoiceTitle="Test3", InvoiceDescription="Test despcription 3", Hours=1.5m, Amount=1.2m, InvoiceDate = DateTime.Parse("2017-07-17"),  DueDate = DateTime.Parse("2017-07-30")}
                };

            invoices.ForEach(invoice => context.Invoices.Add(invoice));
            context.SaveChanges();
        }
    }
}