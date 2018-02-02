using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidloes.Models;
using Vidloes.ViewModels;
using Vidloes.Dtos;
using AutoMapper;

namespace Vidloes.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private VidlyoesContext context;

        public CustomersController()
        {
            context = new VidlyoesContext();
        }

        //Get/api/Customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //Get/api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
            
        }

        //POST/api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                // new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            context.Customers.Add(customer);
            context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT/api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDB = context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDB);

            //customerInDB.Name = customer.Name;
            //customerInDB.BirthDate = customer.BirthDate;
            //customerInDB.IsSubCribedToNewsletter = customer.IsSubCribedToNewsletter;
            //customerInDB.MembershipTypeId = customer.MembershipTypeId;

            context.SaveChanges();

        }



        //DELETE/api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            context.Customers.Remove(customerInDb);
            context.SaveChanges();
        }
    }
}
