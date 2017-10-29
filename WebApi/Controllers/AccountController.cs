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
    public class AccountController : Controller
    {
        // GET: api/account
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            var accounts = JsonConvert.SerializeObject(BankUtilities.GetBankAccounts());

            return new string[] { accounts };
        }

        // GET api/account/2
        [HttpGet("{id}", Name = "GetAccounts")]
        public IActionResult GetById(long id)
        {
            var accounts = JsonConvert.SerializeObject(CustomerUtilities.GetCustomerAccounts(id.ToString()));
            if (accounts == null)
            {
                return NotFound();
            }
            return new ObjectResult(accounts);
        }

        // POST api/account
        [HttpPost]
        public IActionResult Create([FromBody] BankDB.Model.BankAccount account)
        {
            if (account == null)
            {
                return BadRequest();
            }

            CustomerUtilities.AddCustomerBankAccount(account.Iban, account.Name, account.BankId, account.CustomerId, account.Balance.ToString());

            return CreatedAtRoute("GetAccounts", new { id = account.CustomerId }, account);
        }

        // DELETE api/account/ibanstring
        [HttpDelete("{iban}")]
        public IActionResult Delete(string iban)
        {
            CustomerUtilities.DeleteCustomerAccount(iban);

            return new NoContentResult();
        }
    }
}
