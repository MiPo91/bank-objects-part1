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
    public class BankController : Controller
    {
        // GET: api/bank
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            var banks = JsonConvert.SerializeObject(BankUtilities.GetBanks());

            return new string[] {banks};
        }

        // GET api/bank/5
        [HttpGet("{id}", Name = "GetBank")]
        public IActionResult GetById(long id)
        {
            var bank = BankUtilities.GetBanks().FirstOrDefault(b => b.Id == id);
            if (bank == null)
            {
                return NotFound();
            }
            return new ObjectResult(bank);
        }

        // POST api/bank
        [HttpPost]
        public IActionResult Create([FromBody] BankDB.Model.Bank bank)
        {
            if (bank == null)
            {
                return BadRequest();
            }

            var newBank = BankUtilities.AddBank(bank.Name, bank.Bic);

            return CreatedAtRoute("GetBank", new { id = newBank.Id }, bank);
        }

        // PUT api/bank/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] BankDB.Model.Bank updatedBank)
        {
            if (updatedBank == null || updatedBank.Id != id)
            {
                return BadRequest();
            }

            var bank = BankUtilities.GetBanks().FirstOrDefault(b => b.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            bank.Name = updatedBank.Name;
            bank.Bic = updatedBank.Bic;

            BankUtilities.UpdateBank(bank.Id, bank.Name, bank.Bic);

            return new NoContentResult();
        }

        // DELETE api/bank/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var bank = BankUtilities.GetBanks().FirstOrDefault(b => b.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            BankUtilities.DeleteBank(bank.Id);

            return new NoContentResult();
        }
    }
}
