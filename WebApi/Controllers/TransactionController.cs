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
    public class TransactionController : Controller
    {

        // GET api/transaction/ibanstring
        [HttpGet("{iban}", Name = "GetTransactions")]
        public IActionResult GetByString(string iban)
        {
            var customer = CustomerUtilities.GetAccountTransactions(iban);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        // POST api/transaction
        [HttpPost]
        public IActionResult Create([FromBody] BankDB.Model.BankAccountTransaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }

            CustomerUtilities.AddCustomerTransaction(transaction.Iban, transaction.Amount.ToString());

            return CreatedAtRoute("GetTransactions", new { iban = transaction.Iban }, transaction);
        }
    }
}
