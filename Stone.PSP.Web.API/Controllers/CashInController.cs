using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using TransactionService.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stone.PSP.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashInController : ControllerBase
    {
        // GET: api/<CashInController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CashInController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CashInController>
        [HttpPost]
        [Route("ProcessTransactionAsync")]        
        public void ProcessTransactionAsync([FromBody] TransactionViewModel transaction)
        {
        }

        // PUT api/<CashInController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CashInController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
