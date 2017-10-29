using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankDB;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        // GET: api/customer
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            var customers = JsonConvert.SerializeObject(BankUtilities.GetUsers());

            return new string[] { customers };
        }

        // GET api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var customer = BankUtilities.GetUsers().FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        // POST api/customer
        [HttpPost]
        public IActionResult Create([FromBody] BankDB.Model.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var newCustomer = CustomerUtilities.AddCustomer(customer.FirstName, customer.LastName, customer.BankId.ToString());

            return CreatedAtRoute("GetCustomer", new { id = newCustomer.Id }, customer);
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BankDB.Model.Customer updatedCustomer)
        {
            if (updatedCustomer == null || updatedCustomer.Id != id)
            {
                return BadRequest();
            }

            var customer = BankUtilities.GetUsers().FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            CustomerUtilities.UpdateCustomerData(customer.Id.ToString(), customer.FirstName, customer.LastName);

            return new NoContentResult();
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var customer = BankUtilities.GetUsers().FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            CustomerUtilities.DeleteCustomer(customer.Id.ToString());

            return new NoContentResult();
        }
    }
}
