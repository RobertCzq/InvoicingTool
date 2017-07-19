using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InvoicingTool.DAL;
using InvoicingTool.Models;
using Microsoft.AspNet.Identity;

namespace InvoicingTool.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private InvoicingToolContext db = new InvoicingToolContext();

        // GET: Invoice
        public ActionResult Index(string sortOrder, string searchString)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "invoice_desc" : "";
            ViewBag.InvoiceDateSortParm = sortOrder == "invoice_asc" ? "date_desc" : "invoice_asc";
            ViewBag.DueDateSortParm = sortOrder == "due_asc" ? "date_desc" : "due_asc";
            ViewBag.ProjectSortParm = sortOrder == "project_asc" ? "project_desc" : "project_asc";
            ViewBag.CustomerSortParm = sortOrder == "customer_asc" ? "customer_desc" : "customer_asc";

            string userEmail = User.Identity.GetUserName();
            var invoices = db.Invoices.Where(i => i.UserEmail == userEmail)
                                      .Include(i => i.Project)
                                      .Include(i => i.Customer);


            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = invoices.Where(i => i.InvoiceTitle.Contains(searchString)
                                       || i.Project.ProjectName.Contains(searchString)
                                       || i.Customer.CustomerName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "customer_desc":
                    invoices = invoices.OrderByDescending(i => i.Customer.CustomerName);
                    break;
                case "customer_asc":
                    invoices = invoices.OrderBy(i => i.Customer.CustomerName);
                    break;
                case "project_desc":
                    invoices = invoices.OrderByDescending(i => i.Project.ProjectName);
                    break;
                case "project_asc":
                    invoices = invoices.OrderBy(i => i.Project.ProjectName);
                    break;
                case "due_desc":
                    invoices = invoices.OrderByDescending(i => i.DueDate);
                    break;
                case "due_asc":
                    invoices = invoices.OrderBy(i => i.DueDate);
                    break;
                case "invoice_desc":
                    invoices = invoices.OrderByDescending(i => i.InvoiceTitle);
                    break;
                case "invoice_asc":
                    invoices = invoices.OrderBy(i => i.InvoiceDate);
                    break;
                case "date_desc":
                    invoices = invoices.OrderByDescending(i => i.InvoiceDate);
                    break;
                default:
                    invoices = invoices.OrderBy(i => i.InvoiceTitle);
                    break;
            }

            
            return View(invoices.ToList());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,CustomerID,InvoiceTitle,InvoiceDescription,Hours,Amount,InvoiceDate,DueDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.UserEmail = User.Identity.GetUserName();
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", invoice.ProjectID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
         

            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", invoice.ProjectID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", invoice.CustomerID);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,CustomerID,InvoiceTitle,InvoiceDescription,Hours,Amount,InvoiceDate,DueDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.UserEmail = User.Identity.GetUserName();
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "ProjectName", invoice.ProjectID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", invoice.CustomerID);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
