using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidloes.Models;
using Vidloes.ViewModels;

namespace Vidloes.Controllers
{
    public class CustomersController : Controller
    {




        // GET: Customers
        private VidlyoesContext context;

        public CustomersController()
        {
            context = new VidlyoesContext();
        }

        protected override void Dispose(bool disposing)
        {
            // base.Dispose(disposing);
            context.Dispose();
        }


        public ActionResult Index()
        {
            var customers = context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = context.Customers.Include(x => x.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);

        }

        public ActionResult New()
        {
            var membershipTypes = context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewMoedel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewMoedel);
            }

            if(customer.Id==0)
            context.Customers.Add(customer);

            else
            {
                var customerInDb = context.Customers.Single(c => c.Id == customer.Id);
                // TryUpdateModel(customerInDb);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubCribedToNewsletter = customer.IsSubCribedToNewsletter;
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = context.MembershipTypes.ToList()
            };

            return View("CustomerForm",viewModel);
        }
    }
}